using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// コマンド入れ替えクラス
    /// </summary>
    public class CommandSwapManager : MonoBehaviour
    {
        [Header("デバッグ用表示変数群(変更しないでください)")]
        [SerializeField,Tooltip("メインコマンドの入れ替えインデックス")]
        int mainIndexNum = -1;
        [SerializeField,Tooltip("ストレージコマンドの入れ替えインデックス")]
        int storageIndexNum = -1;
        [SerializeField,Tooltip("入れ替えタイプ")]
        private CommandType swapCommandType;

        [SerializeField,Tooltip("ストレージコマンドを管理するクラス")]
        private CommandStorage commandStorage;

        [SerializeField,Tooltip("コマンドボタンを管理するクラス")]
        private CommandButtonManager buttonManager;

        private MainCommand[] mainCommands; // 入れ替え対象のメインコマンド配列を格納する変数

        private bool isChanged;             // 入れ替えの有無を保存しておく変数

        private void Start()
        {
            isChanged = true;                                                      // 入れ替えの有無を初期化
            buttonManager.Intialize(SwitchTypeSet, MainIndexChange, StorageIndexChange);  // ボタン入れ替えクラスを初期化する
        }

        /// <summary>
        /// テキスト更新処理
        /// </summary>
        private void TextRewriting()
        {
            buttonManager.MainButtonTextRewriting(mainCommands);                 // ボタン管理クラスにメインコマンドボタンのテキスト更新を依頼
            buttonManager.StrageButtonTextRewriting(commandStorage.controlCommand);     // ボタン管理クラスにストレージコマンドボタンのテキスト更新を依頼
        }

        /// <summary>
        /// コマンド入れ替え処理
        /// </summary>
        private void CommandSwap()
        {
            if (mainIndexNum < 0 || storageIndexNum < 0) return;                                               // どちらかのインデックスが0未満であるなら早期リターンする
            if (mainIndexNum >= mainCommands.Length) return;
            if (mainCommands[mainIndexNum] == null && commandStorage.controlCommand[storageIndexNum] == null) return; // 対象のメインコマンドとストレージコマンドに値がないなら早期リターンする

            // 入れ替えタイプがメインコマンドであるなら
            if (swapCommandType == CommandType.Command)
            {
                // メインコマンドとストレージコマンドのコマンドタイプが一致しているか、片方がnullである場合
                if ((mainCommands[mainIndexNum] == null && commandStorage.controlCommand[storageIndexNum].GetCommandType() == CommandType.Command) || 
                    (commandStorage.controlCommand[storageIndexNum] == null && mainCommands[mainIndexNum] != null) || 
                    mainCommands[mainIndexNum] != null && commandStorage.controlCommand[storageIndexNum].GetCommandType() == CommandType.Command)
                {
                    MainCommand main = mainCommands[mainIndexNum];                                          // メインコマンドをローカルに保存
                    mainCommands[mainIndexNum] = commandStorage.controlCommand[storageIndexNum] as MainCommand;    // 対象のメインコマンドにストレージコマンドをダウンキャストして代入
                    commandStorage.controlCommand[storageIndexNum] = main;                                         // ストレージコマンドにローカルに保存したメインコマンドを代入

                    TextRewriting();                                                                        // テキスト更新

                    // コマンドインデックスを初期化する
                    mainIndexNum = -1;
                    storageIndexNum = -1;

                    isChanged = true;                                                                       // 変更済みに変更
                }
            }
            else
            {
                if (mainCommands[mainIndexNum] == null) return;                                               // 対象のメインコマンドに値がないなら早期リターンする
                
                if (commandStorage.controlCommand[storageIndexNum] == null ||                                        // ストレージコマンドに値がないか
                    swapCommandType == commandStorage.controlCommand[storageIndexNum].GetCommandType())     // ストレージコマンドのコマンドタイプが入れ替えタイプと一致しているなら
                {
                    // 各コマンドタイプに応じてダウンキャストをし、値を入れ替える
                    switch (swapCommandType)
                    {
                        case CommandType.Value:
                            ValueCommand num = mainCommands[mainIndexNum].value;
                            mainCommands[mainIndexNum].value = commandStorage.controlCommand[storageIndexNum] as ValueCommand;
                            commandStorage.controlCommand[storageIndexNum] = num;
                            break;
                        case CommandType.Axis:
                            AxisCommand axis = mainCommands[mainIndexNum].axis;
                            mainCommands[mainIndexNum].axis = commandStorage.controlCommand[storageIndexNum] as AxisCommand;
                            commandStorage.controlCommand[storageIndexNum] = axis;
                            break;
                    }

                    TextRewriting();                                                           // テキスト更新

                    // コマンドインデックスを初期化する
                    mainIndexNum = -1;
                    storageIndexNum = -1;

                    isChanged = true;                                                          // 変更済みに変更
                }
            }
        }

        /// <summary>
        /// 引数のint値をコマンドタイプに変更し保存する関数
        /// </summary>
        /// <param name="type">コマンドタイプに変更するint値</param>
        public void SwitchTypeSet(int type)
        {
            swapCommandType = (CommandType)type;
        }

        /// <summary>
        /// 有効化処理
        /// </summary>
        /// <param name="obj">入れ替え対象のメインコマンド配列</param>
        public void SwapActivation(MainCommand[] obj)
        {
            mainCommands = obj;                // メインコマンド配列をクラス内に保存

            buttonManager.CanvasDisplay();     // ボタン表示キャンバスを表示する

            TextRewriting();                   // テキスト更新処理

        }

        /// <summary>
        /// 無効化処理
        /// </summary>
        /// <returns>変更が行われたか</returns>
        public bool SwapInvalidation()
        {
            buttonManager.CanvasHide();                     // ボタン表示キャンバスを非表示にする

            if (isChanged)                                  // 変更が行われているなら
            {
                isChanged = false;                          // 変更状況を初期化

                return true;                                // 変更済みであると送信
            }

            return false;                                   // 変更されていないと送信
        }

        /// <summary>
        /// メインコマンドインデックスを登録する関数
        /// </summary>
        /// <param name="index">対象インデックス</param>
        private void MainIndexChange(int index)
        {
            mainIndexNum = index; // メインコマンドインデックスを保存

            CommandSwap();        // コマンド入れ替え関数を実行
        }

        /// <summary>
        /// ストレージコマンドインデックスを登録する関数
        /// </summary>
        /// <param name="index">対象インデックス</param>
        private void StorageIndexChange(int index)
        {
            storageIndexNum = index;  // ストレージコマンドインデックスを保存

            CommandSwap();            // コマンド入れ替え関数を実行
        }
    }
}
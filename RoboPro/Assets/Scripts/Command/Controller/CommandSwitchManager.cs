using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// コマンド入れ替えクラス
    /// </summary>
    public class CommandSwitchManager : MonoBehaviour
    {
        [Header("デバッグ用表示変数群(変更しないでください)")]
        [SerializeField,Tooltip("メインコマンドの入れ替えインデックス")]
        int index_main = -1;
        [SerializeField,Tooltip("ストレージコマンドの入れ替えインデックス")]
        int index_strage = -1;
        [SerializeField,Tooltip("入れ替えタイプ")]
        private CommandType switchType;

        [SerializeField,Tooltip("ストレージコマンドを管理するクラス")]
        private CommandStrage strage;

        [SerializeField,Tooltip("コマンドボタンを管理するクラス")]
        private CommandButtonManager buttonManager;

        private MainCommand[] mainCommands; // 入れ替え対象のメインコマンド配列を格納する変数
        private SaveArchive saveItem;       // セーブ情報クラスを格納する変数

        private bool change = true;         // 入れ替えの有無を保存しておく変数

        private void Start()
        {
            buttonManager.Intialize(SwitchTypeSet, ChoosingMain, ChoosingStrage);  // ボタン入れ替えクラスを初期化する
        }

        /// <summary>
        /// テキスト更新処理
        /// </summary>
        private void TextUpdate()
        {
            buttonManager.MainButtonTextUpdate(mainCommands);       // ボタン管理クラスにメインコマンドボタンのテキスト更新を依頼
            buttonManager.StrageButtonTextUpdate(strage.bases);     // ボタン管理クラスにストレージコマンドボタンのテキスト更新を依頼
        }

        /// <summary>
        /// コマンド入れ替え処理
        /// </summary>
        private void CommandSwitch()
        {
            if ((index_main >= 0 && index_strage >= 0) &&                                      // メインコマンドインデックスとストレージコマンドインデックスの両方に値が存在し、
                (mainCommands[index_main] == strage.bases[index_strage] ||                     // メインコマンドとストレージコマンドが一致しているか
                (strage.bases[index_strage] == null && mainCommands[index_main] != null) ||    // メインコマンドとストレージコマンドのどちらかのみに値が存在するか
                strage.bases[index_strage] != null && mainCommands[index_main] == null ||
                !mainCommands[index_main].CommandNullCheck()))                              // 値のどちらかがnullであるなら
            {
                // コマンドタイプを取得
                CommandType itemType = strage.bases[index_strage] == null ? switchType : strage.bases[index_strage].ConfirmationCommandType();

                // 各コマンドタイプに応じてダウンキャストをし、値を入れ替える
                switch (itemType)
                {
                    case CommandType.Num:
                        NumCommand num = mainCommands[index_main].num;
                        mainCommands[index_main].num = strage.bases[index_strage] as NumCommand;
                        strage.bases[index_strage] = num;
                        break;
                    case CommandType.Axis:
                        AxisCommand axis = mainCommands[index_main].axis;
                        mainCommands[index_main].axis = strage.bases[index_strage] as AxisCommand;
                        strage.bases[index_strage] = axis;
                        break;
                    case CommandType.Command:
                        MainCommand main = mainCommands[index_main];
                        mainCommands[index_main] = strage.bases[index_strage] as MainCommand;
                        strage.bases[index_strage] = main;
                        break;
                }

                TextUpdate();       // テキスト更新

                // コマンドインデックスを初期化する
                index_main = -1;    
                index_strage = -1;

                change = true;      // 変更済みに変更
            }
        }

        /// <summary>
        /// 引数のint値をコマンドタイプに変更し保存する関数
        /// </summary>
        /// <param name="type">コマンドタイプに変更するint値</param>
        public void SwitchTypeSet(int type)
        {
            switchType = (CommandType)type;
        }

        /// <summary>
        /// 有効化処理
        /// </summary>
        /// <param name="obj">入れ替え対象のメインコマンド配列</param>
        /// <param name="item">セーブ情報クラス</param>
        public void SwitchActivate(MainCommand[] obj, SaveArchive item)
        {
            mainCommands = obj;             // メインコマンド配列をクラス内に保存

            saveItem = item;                //  セーブ情報クラスをクラス内に保存

            buttonManager.CanvasDisplay();  // ボタン表示キャンバスを表示する

            TextUpdate();                   // テキスト更新処理

        }

        /// <summary>
        /// 無効化処理
        /// </summary>
        /// <returns>変更が行われたか</returns>
        public bool TextInactive()
        {
            buttonManager.CanvasHide();                     // ボタン表示キャンバスを非表示にする

            if (change)                                     // 変更が行われているなら
            {
                // セーブ情報クラスに各コマンド状況を保存
                saveItem.saveMainCommand = mainCommands;    
                saveItem.saveStrageCommand = strage.bases;

                change = false;                             // 変更状況を初期化

                return true;                                // 変更済みであると送信
            }

            return false;                                   // 変更されていないと送信
        }

        /// <summary>
        /// メインコマンドインデックスを登録する関数
        /// </summary>
        /// <param name="index">対象インデックス</param>
        private void ChoosingMain(int index)
        {
            index_main = index; // メインコマンドインデックスを保存

            CommandSwitch();    // コマンド入れ替え関数を実行
        }

        /// <summary>
        /// ストレージコマンドインデックスを登録する関数
        /// </summary>
        /// <param name="index">対象インデックス</param>
        private void ChoosingStrage(int index)
        {
            index_strage = index;  // ストレージコマンドインデックスを保存

            CommandSwitch();    // コマンド入れ替え関数を実行
        }
    }
}
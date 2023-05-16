using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Command.Entity
{
    /// <summary>
    /// ストレージコマンドを管理するクラス
    /// </summary>
    public class CommandStorage : MonoBehaviour
    {
        [SerializeField,Tooltip("ストレージコマンドの数")]
        private int storageCount = 0;

        public CommandBase[] controlCommand; // 管理しているストレージコマンド(以下管理コマンドと呼ぶ)

        private List<CommandBase[]> storageArchive = new List<CommandBase[]>();

        private void Awake()
        {
            controlCommand = new CommandBase[storageCount];   // 設定された数でストレージコマンド配列を初期化

            CommandBase[] startCommand = new CommandBase[3];

            storageArchive.Add(startCommand);               // 初期コマンドをアーカイブに追加
        }

        /// <summary>
        /// コマンドを登録する関数(追加配列がない場合は先頭要素をコピーします)
        /// </summary>
        /// <param name="index">登録インデックス</param>
        /// <param name="newArray">追加する配列</param>
        public void AddArchiveCommand(int index, CommandBase[] newArray = null)
        {
            if (index > storageArchive.Count) return;                                   // 登録インデックスが範囲外なら早期リターンする

            controlCommand = newArray;                                                  // 管理コマンドを追加配列に変更

            CommandBase[] copyArray = new CommandBase[newArray.Length];                 // リストに追加する用のローカル配列を作成

            for (int i = 0; i < newArray.Length; i++)                                   // 追加配列の要素数分実行
            {
                copyArray[i] = newArray[i] == null ? default : newArray[i].BaseClone(); // ローカル配列のコピーを作成する
            }

            if (storageArchive.Count != index)                                          // 管理コマンドの要素数と異なるのであれば
            {                                                                           // 不要となるのでそれまでの要素をリストから破棄する
                for (int i = storageArchive.Count - 1; i >= index; i--)                 // コマンドアーカイブの過剰分実行
                {
                    storageArchive.RemoveAt(i);                                         // その要素をリストから破棄する
                }
            }

            storageArchive.Add(copyArray);                                              // コマンドアーカイブにローカル配列を追加
        }

        /// <summary>
        /// 管理コマンドを対象の要素番号のアーカイブ情報に書き換える
        /// </summary>
        /// <param name="index">対象の要素番号</param>
        public void OverwriteControlCommand(int index)
        {
            CommandBase[] copyArray = new CommandBase[controlCommand.Length];   // 管理コマンドに書き換えるためのローカル配列を作成(配列を直で渡すと参照を渡すため)

            for (int i = 0; i < storageArchive[index].Length; i++)              // 対象のコマンドアーカイブの要素数分実行
            {
                // ローカル配列に各要素のコピーを作成(値が存在しない場合はデフォルト値を代入)
                copyArray[i] = storageArchive[index][i] == null ? default : storageArchive[index][i].BaseClone(); 
            }

            controlCommand = copyArray;                                         // 管理コマンドをローカル配列に変更
        }
    }
}
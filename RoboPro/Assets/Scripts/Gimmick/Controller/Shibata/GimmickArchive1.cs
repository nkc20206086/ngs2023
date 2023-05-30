using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Entity;

namespace Gimmick
{
    /// <summary>
    /// ギミックのセーブ情報を格納する構造体
    /// </summary>
    public struct GimmickArchive1
    {
        public MainCommand[] controlCommand { get; private set; }   // 管理コマンド
        private MainCommand[] playCommand;                          // 実行コマンド
        private int playIndex;                                      // 実行インデックス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="control">管理コマンド</param>
        /// <param name="play">実行コマンド</param>
        /// <param name="state">実行状態</param>
        /// <param name="playIndex">実行インデックス</param>
        public GimmickArchive1(MainCommand[] control,MainCommand[] play,int playIndex)
        {
            MainCommand[] controlCopy = new MainCommand[control.Length];    // 管理コマンドを保存するための配列を作成
            MainCommand[] playCopy = new MainCommand[play.Length];          // 実行コマンドを保存するための配列を作成

            // 管理コマンドの内容をコピーして格納
            for (int i = 0;i < control.Length;i++)
            {
                controlCopy[i] = control[i] != null ? control[i].MainCommandClone() : default;
            }

            // 実行コマンドの内容をコピーして格納
            for (int i = 0;i < play.Length;i++)
            {
                playCopy[i] = play[i] != null ? play[i].MainCommandClone() : default;
            }

            // 各項目を記録
            controlCommand = controlCopy;
            playCommand = playCopy;
            this.playIndex = playIndex;
        }

        /// <summary>
        /// 各項目を記録されている内容に書き換える関数
        /// </summary>
        /// <param name="control">管理コマンド</param>
        /// <param name="play">実行コマンド</param>
        /// <param name="playIndex">実行インデックス</param>
        public void SetGimmickArchive(MainCommand[] control,MainCommand[] play,int playIndex)
        {
            // 管理コマンドに記録内容のコピーを渡す
            for (int i = 0;i < controlCommand.Length;i++)
            {
                control[i] = controlCommand[i] != null ? controlCommand[i].MainCommandClone() : default;
            }

            // 実行コマンドに記録内容のコピーを渡す
            for (int i = 0;i < playCommand.Length;i++)
            {
                play[i] = playCommand[i] != null ? playCommand[i].MainCommandClone() : default;
            }

            // 各項目を書き換える
            playIndex = this.playIndex;
        }
    }
}
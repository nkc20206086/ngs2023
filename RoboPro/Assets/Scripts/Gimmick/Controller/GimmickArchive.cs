using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Entity;

namespace Gimmick
{
    /// <summary>
    /// ギミックのセーブ情報を格納する構造体
    /// </summary>
    public struct GimmickArchive 
    {
        public MainCommand[] controlCommand { get; private set; }   // 管理コマンド
        private MainCommand[] playCommand;                          // 実行コマンド
        private Vector3 position;                                   // 座標
        private Quaternion rotation;                                // 回転
        private Vector3 scale;                                      // 大きさ
        private CommandState state;                                 // 実行状態
        private int playIndex;                                      // 実行インデックス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="control">管理コマンド</param>
        /// <param name="play">実行コマンド</param>
        /// <param name="transform">座標</param>
        /// <param name="state">実行状態</param>
        /// <param name="playIndex">実行インデックス</param>
        public GimmickArchive(MainCommand[] control,MainCommand[] play,Transform transform,CommandState state,int playIndex)
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
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
            this.state = state;
            this.playIndex = playIndex;
        }

        /// <summary>
        /// 各項目を記録されている内容に書き換える関数
        /// </summary>
        /// <param name="control">管理コマンド</param>
        /// <param name="play">実行コマンド</param>
        /// <param name="transform">座標</param>
        /// <param name="state">実行状態</param>
        /// <param name="playIndex">実行インデックス</param>
        public void SetGimmickArchive(MainCommand[] control,MainCommand[] play,Transform transform,out CommandState state,out int playIndex)
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
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
            state = this.state;
            playIndex = this.playIndex;
        }
    }
}
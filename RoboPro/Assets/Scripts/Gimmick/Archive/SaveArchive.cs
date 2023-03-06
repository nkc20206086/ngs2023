using UnityEngine;
using System;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// セーブ情報クラス
    /// </summary>
    public class SaveArchive
    {
        public int index { get; private set; }                  // このクラスが持つインデックス情報
        public Transform saveTransform { get; private set; }    // 対象の座標値等

        private MainCommand[] mainCommands;                     // メインコマンド配列
        private CommandBase[] strageCommands;                   // ストレージコマンド配列

        /// <summary>
        /// セーブされるメインコマンド配列
        /// </summary>
        public MainCommand[] saveMainCommand
        {
            get => mainCommands;                                        // クラス内で保存されているメインコマンド配列を返す
            set
            {
                MainCommand[] commands = new MainCommand[value.Length]; // メインコマンド配列のローカル変数を作成する

                // メインコマンド配列のローカル変数に、代入された配列のコピーを作成する
                for (int i = 0;i < commands.Length;i++)
                {
                    commands[i] = value[i] != null ? value[i].MainCommandClone() : default;
                }

                mainCommands = commands;                                // 作成したローカル変数をクラス内に保存
            }
        }

        /// <summary>
        /// セーブされているストレージコマンド配列
        /// </summary>
        public CommandBase[] saveStrageCommand
        {
            get => strageCommands;                                      // クラス内で保存されているストレージコマンド配列を返す
            set
            {
                CommandBase[] commands = new CommandBase[value.Length]; // ストレージコマンド配列のローカル変数を作成する

                // ストレージコマンド配列のローカル変数に代入された配列のコピーを作成する
                for (int i = 0;i < commands.Length;i++)
                {
                    commands[i] = value[i] != null ? value[i].BaseClone() : default;
                }

                strageCommands = commands;                              // 作成したローカル変数をクラス内に保存
            }
        }

        /// <summary>
        /// コンストラクタ(インデックス、トランスフォーム設定用)
        /// </summary>
        /// <param name="index">保存インデックス</param>
        /// <param name="transform">保存トランスフォーム</param>
        public SaveArchive(int index, Transform transform)
        {
            // 各値を設定する
            this.index = index;
            saveTransform = transform;

            // コマンド配列は後からの設定となるためデフォルト値を設定する
            mainCommands = default;
            strageCommands = default;
        }
    }
}
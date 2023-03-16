using UnityEngine;
using System;
using Command;
using Command.Entity;
using System.Collections.Generic;

namespace Gimmick
{
    /// <summary>
    /// ギミック管理クラス
    /// </summary>
    public class GimmckController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[3];               // このギミックの持つメインコマンド配列

        private int playIndex = -1;                                             // このギミックの生成インデックス
        private float timeCount = 0;                                            // 時間計測用変数
        private MainCommand[] playCommand = new MainCommand[3];                 // 実行コマンドの配列
        private List<MainCommand[]> commandArchive = new List<MainCommand[]>(); // 変更を保存していくコマンドアーカイブ

        [Header("値確認用 数値変更非推奨")]
        [SerializeField]
        private bool isExecutable = true;                                       // 実行可能か
        [SerializeField]
        private CommandState state;                                             // 動き方の状態変数

        // Start is called before the first frame update
        void Start()
        {
            // 初期値を設定(現時点でどのようなコマンドを設定する機能をつけていないため)
            CommandStruct st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.X, 1);
            controlCommand[0] = CommandCreater.CreateCommand(st);
            st = new CommandStruct(MainCommandType.Rotate, false, false, false, 90, CoordinateAxis.Z, 1);
            controlCommand[1] = CommandCreater.CreateCommand(st);
            st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.Y, 1);
            controlCommand[2] = CommandCreater.CreateCommand(st);

            state = CommandState.MOVE_ON;                                   // コマンドステートを通常移動にする

            Array.Copy(controlCommand, playCommand, controlCommand.Length); // 管理コマンドに実行コマンドの内容をコピー

            IndexSwitching();                                               // 実行インデックス変更用関数

            // 実行コマンドリストの要素全てに初期化関数を実行
            foreach (MainCommand command in playCommand)                    
            {
                command.StartUp();
            }

            MainCommand[] copyCommand = new MainCommand[3];
            for (int i = 0;i < copyCommand.Length;i++)
            {
                copyCommand[i] = controlCommand[i].MainCommandClone();
            }

            commandArchive.Add(copyCommand);                             // 初期コマンドをアーカイブに追加する
        }

        public void CommandUpdate()
        {
            if (!isExecutable)                  // 実行されていないなら実行可能かをチェックする
            {
                CheckExecutable();
                return;
            }

            if (timeCount > 0)                  // 時間計測用変数が0以上の数値を持つなら
            {
                timeCount -= Time.deltaTime;    // 時間計測用変数を減らす
                if (timeCount < 0)              // これにより0未満になった場合
                {
                    timeCount = 0.0f;           // 計測用変数を初期化する
                    IndexSwitching();           // 実行インデックス変更用関数
                }
            }
            else　                              // 時間計測用変数が値を持たない場合
            {
                // 現在の実行インデックスのコマンドを実行
                playCommand[playIndex].CommandExecute(state, transform);
            }
        }

        /// <summary>
        /// 指定インデックスのアーカイブにコマンドリストを追加する
        /// </summary>
        /// <param name="index">登録インデックス</param>
        public void AddNewCommandsToArchive(int index)
        {
            if (index > commandArchive.Count) return;                           // 要素数を超えているなら追加できないため早期リターンする

            MainCommand[] copyArray = new MainCommand[controlCommand.Length];   // リストに追加する用のローカル配列を作成

            for (int i = 0;i < commandArchive[commandArchive.Count - 1].Length;i++)
            {
                // 同じ要素番号の要素をコピーする(値が存在しないならデフォルト値を作成)
                copyArray[i] = commandArchive[commandArchive.Count - 1][i] == null ? default : commandArchive[commandArchive.Count - 1][i].MainCommandClone(); 
            }

            if (commandArchive.Count != index)                                  // 要素番号とコマンドアーカイブの要素数が異なるなら(要素数より大きい数値は早期リターンされるため最大数か否かの判別となる)
            {
                for (int i = commandArchive.Count - 1;i >= index;i--)
                {
                    // 余剰分の要素をすべて破棄する
                    commandArchive.RemoveAt(i);                                 
                }
            }

            commandArchive.Add(copyArray);                                      // コピーした配列をコマンドアーカイブに加える
        }

        /// <summary>
        /// 現在管理しているコマンドをコマンドアーカイブに登録する
        /// </summary>
        /// <param name="index">登録インデックス</param>
        public void AddControlCommandToArchive(int index)
        {
            if (index > commandArchive.Count) return;                           // 要素数を超えているなら追加できないため早期リターンする

            MainCommand[] copyArray = new MainCommand[controlCommand.Length];   // リストに追加する用のローカルの配列を作成

            for (int i = 0; i < controlCommand.Length; i++)
            {
                // 同じ要素番号の要素をコピーする(値が存在しないならデフォルト値を作成)
                copyArray[i] = controlCommand[i] == null ? default : controlCommand[i].MainCommandClone();
            }

            if (commandArchive.Count != index)                                  // 要素番号と最大数が異なるなら
            {
                for (int i = commandArchive.Count - 1; i >= index; i--)
                {
                    // 余剰分の要素をすべて破棄する
                    commandArchive.RemoveAt(i);
                }
            }

            commandArchive.Add(copyArray);                                      // コピーした配列をコマンドアーカイブに加える
        }

        /// <summary>
        /// 指定インデックスのコマンドアーカイブ内容を管理コマンドに反映する関数
        /// </summary>
        /// <param name="index">対象のインデックス</param>
        public void OverwriteControlCommand(int index)
        {
            MainCommand[] copyArray = new MainCommand[controlCommand.Length];   // 管理コマンド書き換え用のローカル配列を作成する

            for (int i = 0; i < commandArchive[index].Length; i++)
            {
                // 書き換え用配列にコマンドアーカイブの[指定インデックス][要素番号]の内容をコピーして入れていく(値が存在しないならデフォルト値を設定)
                copyArray[i] = commandArchive[index][i] == null ? default : commandArchive[index][i].MainCommandClone(); 
            }

            controlCommand = copyArray;                                         // 管理コマンドを書き換え用配列に変更
        }

        /// <summary>
        /// 実行インデックス変更用関数
        /// </summary>
        private void IndexSwitching()
        {
            if (state == CommandState.MOVE_ON)      // 通常移動であればインデックスを加算
            {
                playIndex++;
            }
            else                                    // 反転移動であればインデックスを減算
            {
                playIndex--;
            }

            if (playCommand.Length <= playIndex)    // 実行インデックスが管理コマンドの数を超えたら              
            {
                state = CommandState.RETURN;        // コマンドステートを反転移動にする
                playIndex--;                        // 実行インデックスを減算する
            }
            else if (playIndex < 0)                 // 実行インデックスが0未満であるなら
            {
                playIndex = 0;                      // 実行インデックスを0に設定
                state = CommandState.MOVE_ON;       // コマンドステートを通常移動に変更
                CheckExecutable();                  // 実行可能であるかを確認 
            }

            // 対象のコマンドの有効化処理を行う
            playCommand[playIndex]?.ActionActivate(CreateInterval, gameObject);
        }

        /// <summary>
        /// コマンド終了時にインターバルを設けるための処理
        /// </summary>
        private void CreateInterval()
        {
            timeCount = 1.0f;
        }

        /// <summary>
        /// 実行可能であるかを確認する関数
        /// </summary>
        private void CheckExecutable()
        {
            if (controlCommand.Length <= 0)         // コマンド管理数が0を下回るなら
            {
                isExecutable = false;               // 実行不可に変更
                playCommand = new MainCommand[3];   // 実行コマンドの中身を空にする
                return;
            }
            else　                                   
            {
                // 管理コマンドの要素に実行不可であるものが含まれれば、実行不可に変更し早期リターンする
                foreach (MainCommand command in controlCommand)
                {
                    if (command == null || !command.CommandNullCheck())
                    {
                        isExecutable = false;
                        playCommand = new MainCommand[3];
                        return;
                    }
                }

                isExecutable = true;                                            // 実行可能に変更
                Array.Copy(controlCommand, playCommand, controlCommand.Length); // 管理コマンドに実行コマンドの内容をコピー

                // 実行コマンドリストの要素全てに初期化関数を実行
                foreach (MainCommand command in playCommand)
                {
                    command.StartUp();
                }

                playCommand[0]?.ActionActivate(CreateInterval, gameObject);     // 先頭要素に有効化関数を実行
            }
        }
    }
}
using UnityEngine;
using System;
using Command;
using Command.Entity;

namespace Gimmick
{
    /// <summary>
    /// ギミック管理クラス
    /// </summary>
    public class GimmckController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[3];   // このギミックの持つメインコマンド配列

        private int index = -1;                                     // このギミックの生成インデックス
        private float count = 0;                                    // 時間計測用変数
        private MainCommand[] playCommand = new MainCommand[3];     // 実行コマンドの配列

        [Header("値確認用 数値変更非推奨")]
        [SerializeField]
        private bool play = true;

        [SerializeField]
        private CommandState state;


        // Start is called before the first frame update
        void Start()
        {
            // 初期値を設定(現時点でどのようなコマンドを設定する機能をつけていないため)
            CommandStruct st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.X, 1);
            controlCommand[0] = (CommandCreater.CreateCommand(st));
            st = new CommandStruct(MainCommandType.Rotate, false, false, false, 90, CoordinateAxis.Z, 1);
            controlCommand[1] = (CommandCreater.CreateCommand(st));
            st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.Y, 1);

            controlCommand[2] = (CommandCreater.CreateCommand(st));
            state = CommandState.MOVE_ON;                                   // コマンドステートを通常移動にする

            Array.Copy(controlCommand, playCommand, controlCommand.Length); // 管理コマンドに実行コマンドの内容をコピー

            OnExecute();                                                    // 実行インデックス変更用関数

            // 実行コマンドリストの要素全てに初期化関数を実行
            foreach (MainCommand command in playCommand)                    
            {
                command.StartUp();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!play)                      // 実行されていないなら実行可能かをチェックする
            {
                PlayCheck();
                return;
            }

            if (count > 0)                  // 時間計測用変数が0以上の数値を持つなら
            {
                count -= Time.deltaTime;    // 時間計測用変数を減らす
                if (count < 0)              // これにより0未満になった場合
                {
                    count = 0.0f;           // 計測用変数を初期化する
                    OnExecute();            // 実行インデックス変更用関数
                }
            }
            else　                          // 時間計測用変数が値を持たない場合
            {
                // 現在の実行インデックスのコマンドを実行
                playCommand[index].CommandExecute(state, transform);
            }
        }

        /// <summary>
        /// 実行インデックス変更用関数
        /// </summary>
        private void OnExecute()
        {
            if (state == CommandState.MOVE_ON)  // 通常移動であればインデックスを増加
            {
                index++;
            }
            else                                // 反転移動であればインデックスを減算
            {
                index--;
            }

            if (playCommand.Length <= index)    // 実行インデックスが管理コマンドの数を超えたら              
            {
                state = CommandState.RETURN;    // コマンドステートを反転移動にする
                index--;                        // 実行インデックスを減算する
                // 対象のコマンドの有効化処理を行う
                playCommand[index].ActionActivate(Complete, gameObject);
            }
            else if (index < 0)                 // 実行インデックスが0未満であるなら
            {
                index = 0;                      // 実行インデックスを0に設定
                state = CommandState.MOVE_ON;   // コマンドステートを通常移動に変更
                PlayCheck();                    // 実行可能であるかを確認
                // 対象のコマンドが存在すれば有効化処理を行う
                playCommand[index]?.ActionActivate(Complete, gameObject);   
            }
            else
            {
                // 対象のコマンドの有効化処理を行う
                playCommand[index].ActionActivate(Complete, gameObject);
            }
        }

        /// <summary>
        /// コマンド終了時にインターバルを設けるための処理
        /// </summary>
        private void Complete()
        {
            count = 1.0f;
        }

        /// <summary>
        /// 実行可能であるかを確認する関数
        /// </summary>
        private void PlayCheck()
        {
            if (controlCommand.Length <= 0)         // コマンド管理数が0を下回るなら
            {
                play = false;                       // 実行不可に変更
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
                        play = false;
                        playCommand = new MainCommand[3];
                        return;
                    }
                }

                play = true;                                                    // 実行可能に変更
                Array.Copy(controlCommand, playCommand, controlCommand.Length); // 管理コマンドに実行コマンドの内容をコピー

                // 実行コマンドリストの要素全てに初期化関数を実行
                foreach (MainCommand command in playCommand)
                {
                    command.StartUp();
                }

                playCommand[0]?.ActionActivate(Complete, gameObject);           // 先頭要素に有効化関数を実行
            }
        }
    }
}
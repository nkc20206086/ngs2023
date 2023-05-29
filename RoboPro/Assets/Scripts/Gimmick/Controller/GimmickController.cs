using UnityEngine;
using System;
using Command;
using Command.Entity;
using System.Collections.Generic;

namespace Gimmick
{
    // 実行コマンドと管理コマンドが分かれている設計になっている
    // これはUndoとRedoによる値の入れ替えが生じた際、実際に動かすコマンドと管理しているコマンドが異なる可能性があるため

    /// <summary>
    /// ギミック管理クラス
    /// </summary>
    public class GimmickController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[CommandUtility.commandCount]; // 管理コマンドの配列

        [SerializeField]
        private int playIndex = -1;                                                         // 実行されているコマンドのインデックス
        private float timeCount = 0;                                                        // 時間計測用変数
        private MainCommand[] playCommand = new MainCommand[CommandUtility.commandCount];   // 実行コマンドの配列
        private List<GimmickArchive> gimmickArchives = new List<GimmickArchive>();          // 記録用構造体のリスト

        // 各実行時点での原点
        private Vector3 basePos;
        private Quaternion baseQuat;
        private Vector3 baseScale;

        [Header("値確認用 数値変更非推奨")]
        [SerializeField]
        private bool isExecutable = false;                                                  // 実行可能か
        [SerializeField]
        private CommandState state;                                                         // 動き方の状態変数

        public bool GetExecutionStatus { get => isExecutable; }

        /// <summary>
        /// ギミックの開始時関数
        /// </summary>
        /// <param name="setCommands">このギミックの持つコマンド</param>
        public void StartUp(CommandContainer[] setCommands)
        {
            controlCommand = new MainCommand[setCommands.Length];           // 管理コマンド初期化
            playCommand = new MainCommand[setCommands.Length];

            for (int i = 0;i < setCommands.Length;i++)
            {
                // 構造体をメインコマンドクラスに変換し、管理コマンドに設定する
                controlCommand[i] = CommandCreater.CreateCommand(setCommands[i]);
            }

            Array.Copy(controlCommand, playCommand, controlCommand.Length); // 管理コマンドに実行コマンドの内容をコピー

            state = CommandState.MOVE_ON;                                   // コマンドステートを通常移動にする

            IndexSwitching();                                               // 実行インデックス変更用関数

            basePos = transform.position;
            baseQuat = transform.rotation;
            baseScale = transform.localScale;

            CommandCalc();

            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand,playCommand,playIndex);
            gimmickArchives.Add(gimmickArchive);
        }

        /// <summary>
        /// コマンドの実行関数
        /// </summary>
        public void CommandUpdate()
        {
            if (!isExecutable) return;          // 実行不可であれば早期リターンする

            if (playCommand.Length >= CommandUtility.specialCommandNumber)
            {
                SpecialCommandUpdate();
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
            if (index > gimmickArchives.Count) return;                      // 登録インデックスが要素数を超えているなら早期リターンする

            if (gimmickArchives.Count != index)                             // 要素数と登録インデックスが異なるなら(超えている場合は早期リターンされるので、実質的に同じものでない場合)
            {
                for (int i = gimmickArchives.Count - 1; i >= index; i--)    
                {
                    // 超えている要素をすべてリストから削除する
                    gimmickArchives.RemoveAt(i);
                }
            }

            // 記録用の内容を構造体にまとめる
            GimmickArchive gimmickArchive = new GimmickArchive(gimmickArchives[gimmickArchives.Count - 1].controlCommand, playCommand, playIndex);
            gimmickArchives.Add(gimmickArchive);                            // 記録をリストに追加する
        }

        /// <summary>
        /// 現在管理しているコマンドをコマンドアーカイブに登録する
        /// </summary>
        /// <param name="index">登録インデックス</param>
        public void AddControlCommandToArchive(int index)
        {
            if (index > gimmickArchives.Count) return;                      // 登録インデックスが要素数を超えているなら早期リターンする

            if (gimmickArchives.Count != index)                             // 要素数と登録インデックスが異なるなら(超えている場合は早期リターンされるので、実質的に同じものでない場合)
            {
                for (int i = gimmickArchives.Count - 1; i >= index; i--)
                {
                    // 超えている要素をすべてリストから削除する
                    gimmickArchives.RemoveAt(i);
                }
            }

            // 記録用の内容を構造体にまとめる
            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand, playCommand, playIndex);
            gimmickArchives.Add(gimmickArchive);                            // 記録をリストに追加する
        }

        /// <summary>
        /// 指定インデックスのコマンドアーカイブ内容を管理コマンドに反映する関数
        /// </summary>
        /// <param name="index">対象のインデックス</param>
        public void OverwriteControlCommand(int index)
        {
            gimmickArchives[index].SetGimmickArchive(controlCommand,playCommand,playIndex);

            timeCount = 0.0f;

            CommandCalc();
        }

        public void StartingAction(CommandState playState)
        {
            if (playState == CommandState.MOVE_ON)
            {
                CheckExecutable();
                isExecutable = true;
                playIndex = 0;
            }
            else if (playState == CommandState.RETURN)
            {
                if (state != CommandState.MOVE_ON) return;
                isExecutable = true;
                playIndex = playCommand.Length - 1;
            }

            state = playState;

            if (playCommand[playIndex] == null ||
                playCommand[playIndex].GetMainCommandType() == MainCommandType.None ||
                !playCommand[playIndex].CommandNullCheck())
            {
                IndexSwitching();
            }
        }

        public void IntializeAction()
        {
            state = CommandState.INACTIVE;
            isExecutable = false;
            playIndex = 0;
            transform.position = basePos;
            transform.rotation = baseQuat;
            transform.localScale = baseScale;
        }

        /// <summary>
        /// 実行インデックス変更用関数
        /// </summary>
        private void IndexSwitching()
        {
            if (state == CommandState.MOVE_ON)              // 通常移動であればインデックスを加算
            {
                playIndex++;
                if (playCommand.Length <= playIndex)
                {
                    playIndex = playCommand.Length - 1;
                    isExecutable = false;
                }
                else if (playCommand[playIndex] == null ||
                         playCommand[playIndex].GetMainCommandType() == MainCommandType.None || 
                         !playCommand[playIndex].CommandNullCheck())
                {
                    IndexSwitching();
                }
            }
            else                                            // 反転移動であればインデックスを減算
            {
                playIndex--;
                if (playIndex < 0)
                {
                    playIndex = 0;
                    isExecutable = false;
                }
                else if (playCommand[playIndex] == null ||
                         playCommand[playIndex].GetMainCommandType() == MainCommandType.None ||
                         !playCommand[playIndex].CommandNullCheck())
                {
                    IndexSwitching();
                }
            }
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
            if (controlCommand.Length <= 0)                                     // コマンド管理数が0を下回るなら
            {
                isExecutable = false;                                           // 実行不可に変更
                playCommand = new MainCommand[controlCommand.Length];           // 実行コマンドの中身を空にする
                return;
            }
            else　                                   
            {
                isExecutable = true;                                            // 実行可能に変更
                Array.Copy(controlCommand, playCommand, controlCommand.Length); // 管理コマンドに実行コマンドの内容をコピー

                CommandCalc();
            }
        }

        /// <summary>
        /// SPコマンドによる効果の関数
        /// </summary>
        protected virtual void SpecialCommandUpdate() 
        {
            if (playCommand[3] == null) return;
            if (playCommand[3].GetMainCommandType() == MainCommandType.Move)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
        }

        private void CommandCalc()
        {
            Vector3 move = Vector3.zero;
            Quaternion rotation = baseQuat;
            Vector3 scale = baseScale;

            // 実行コマンドリストの要素全てに初期化関数を実行
            for (int i = 0; i < playCommand.Length; i++)
            {
                if (playCommand[i] == null ||
                    !playCommand[i].CommandNullCheck()) continue;

                switch (playCommand[i]?.GetMainCommandType())
                {
                    case MainCommandType.Move:
                        Vector3 moveValue = (Vector3)playCommand[i].InitCommand(basePos + move, CreateInterval);
                        move += moveValue;
                        break;
                    case MainCommandType.Rotate:
                        Quaternion rotateValue = (Quaternion)playCommand[i].InitCommand(rotation, CreateInterval);
                        rotation = rotateValue;
                        break;
                    case MainCommandType.Scale:
                        Vector3 scaleValue = (Vector3)playCommand[i].InitCommand(scale, CreateInterval);
                        scale = scaleValue;
                        break;
                }
            }
        }
    }
}
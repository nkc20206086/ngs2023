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
    public class GimmckController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[3];                       // 管理コマンドの配列

        private int playIndex = -1;                                                     // 実行されているコマンドのインデックス
        private float timeCount = 0;                                                    // 時間計測用変数
        private MainCommand[] playCommand = new MainCommand[3];                         // 実行コマンドの配列
        private List<GimmickArchive> gimmickArchives = new List<GimmickArchive>();      // 記録用構造体のリスト

        // 各実行時点での原点
        private Vector3 basePos;
        private Quaternion baseQuat;
        private Vector3 baseScale;

        [Header("値確認用 数値変更非推奨")]
        [SerializeField]
        private bool isExecutable = true;                                       // 実行可能か
        [SerializeField]
        private CommandState state;                                             // 動き方の状態変数

        /// <summary>
        /// ギミックの開始時関数
        /// </summary>
        /// <param name="setCommands">このギミックの持つコマンド</param>
        public void StartUp(CommandStruct[] setCommands)
        {
            controlCommand = new MainCommand[setCommands.Length];           // 管理コマンド初期化

            for (int i = 0;i < setCommands.Length;i++)
            {
                // 構造体をメインコマンドクラスに変換し、管理コマンドに設定する
                controlCommand[i] = CommandCreater.CreateCommand(setCommands[i]);
            }

            state = CommandState.MOVE_ON;                                   // コマンドステートを通常移動にする

            Array.Copy(controlCommand, playCommand, controlCommand.Length); // 管理コマンドに実行コマンドの内容をコピー

            IndexSwitching();                                               // 実行インデックス変更用関数

            basePos = transform.position;
            baseQuat = transform.rotation;
            baseScale = transform.localScale;

            Vector3 move = Vector3.zero;
            Quaternion rotation = baseQuat;
            Vector3 scale = baseScale;
            // 実行コマンドリストの要素全てに初期化関数を実行
            foreach (MainCommand command in playCommand)
            {
                switch (command.GetMainCommandType())
                {
                    case MainCommandType.Move:
                        Vector3 moveValue = (Vector3)command.StartUp(basePos + move, CreateInterval);
                        move += moveValue;
                        break;
                    case MainCommandType.Rotate:
                        Quaternion rotateValue = (Quaternion)command.StartUp(rotation, CreateInterval);
                        rotation = rotateValue;
                        break;
                }
            }

            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand,playCommand,transform,state,playIndex);
            gimmickArchives.Add(gimmickArchive);
        }

        /// <summary>
        /// コマンドの実行関数
        /// </summary>
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
            GimmickArchive gimmickArchive = new GimmickArchive(gimmickArchives[gimmickArchives.Count - 1].controlCommand, playCommand, transform, state, playIndex);
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
            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand, playCommand, transform, state, playIndex);
            gimmickArchives.Add(gimmickArchive);                            // 記録をリストに追加する
        }

        /// <summary>
        /// 指定インデックスのコマンドアーカイブ内容を管理コマンドに反映する関数
        /// </summary>
        /// <param name="index">対象のインデックス</param>
        public void OverwriteControlCommand(int index)
        {
            gimmickArchives[index].SetGimmickArchive(controlCommand,playCommand,transform,out state,out playIndex);

            timeCount = 0.0f;

            Vector3 move = Vector3.zero;
            Quaternion rotation = baseQuat;
            Vector3 scale = baseScale;
            // 実行コマンドリストの要素全てに初期化関数を実行
            foreach (MainCommand command in playCommand)
            {
                switch (command.GetMainCommandType())
                {
                    case MainCommandType.Move:
                        Vector3 moveValue = (Vector3)command.StartUp(basePos + move, CreateInterval);
                        move += moveValue;
                        break;
                    case MainCommandType.Rotate:
                        Quaternion rotateValue = (Quaternion)command.StartUp(rotation, CreateInterval);
                        rotation = rotateValue;
                        break;
                }
            }
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

                Vector3 move = Vector3.zero;
                Quaternion rotation = baseQuat;
                Vector3 scale = baseScale;
                // 実行コマンドリストの要素全てに初期化関数を実行
                foreach (MainCommand command in playCommand)
                {
                    switch (command.GetMainCommandType())
                    {
                        case MainCommandType.Move:
                            Vector3 moveValue = (Vector3)command.StartUp(basePos + move,CreateInterval);
                            move += moveValue;
                            break;
                        case MainCommandType.Rotate:
                            Quaternion rotateValue = (Quaternion)command.StartUp(rotation,CreateInterval);
                            rotation = rotateValue;
                            break;
                    }
                }
            }
        }
    }
}
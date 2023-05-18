using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Command;
using Command.Entity;
using Gimmick.Interface;

namespace Gimmick
{
    /// <summary>
    /// ギミック関連の処理を管理するクラス
    /// </summary>
    public class GimmickDirector : MonoBehaviour,IGimmickAccess
    {
        [SerializeField,Tooltip("ストレージコマンド管理クラス")]
        private CommandStorage storage;

        [SerializeField,Tooltip("コマンド管理クラス")]
        private CommandDirector commandDirector;

        [Header("デバッグ用　本来であれば生成済みのものは利用しない")]
        [SerializeField]
        private List<GimmckController> instanceGimmickController;
        [SerializeField]
        private List<AccessPoint> accessPoints;

        [Header("値確認用　数値変更非推奨")]
        [SerializeField]
        private int archiveIndex = 0;           // 現在のセーブ状況の何番目を実行しているかのインデックス(以下セーブ参照インデックスと呼ぶ)
        [SerializeField]
        private int swappingGimmickIndex = -1;  // 入れ替え中のギミックインデックス
        [SerializeField]
        private int maxArchiveCount = 0;      　// 記録している入れ替えの数

        private bool isSwapping = false;        // コマンド入れ替え実行中であるかを管理する変数 
        private bool isExecute = false;
        private CommandState state = CommandState.INACTIVE;

        private void Update()
        {
            if (isExecute)
            {
                if (!isSwapping)
                {
                    foreach (GimmckController gimmckController in instanceGimmickController)
                    {
                        gimmckController.CommandUpdate();   // 各ギミックのコマンドを実行する
                    }

                    foreach (GimmckController gimmckController in instanceGimmickController)
                    {
                        if (gimmckController.GetExecutionStatus) return;
                    }

                    isExecute = false;
                }
            }
            else if (isSwapping && Input.GetKeyDown(KeyCode.Escape))
            {
                Close(default);
            }
        }

        /// <summary>
        /// ギミックインスタンス処理
        /// </summary>
        public void GimmickInstance(List<CommandStruct[]> setCommandList)
        {
            // 各要素に入れ替えの開始処理と終了処理を預け、生成インデックスを登録する
            for (int i = 0;i <  instanceGimmickController.Count;i++)
            {
                accessPoints[i].index = i;

                instanceGimmickController[i].StartUp(setCommandList[i]);
            }
        }

        /// <summary>
        /// コマンド入れ替え処理終了
        /// </summary>
        private void Close(Unit unit)
        {
            if (!isSwapping) return;                                                            // 入れ替え実行中でないなら早期リターンする
            isSwapping = false;                                                                 // 入れ替え終了に変更

            bool isSwap = commandDirector.CommandInvalidation();                                // コマンド管理クラスに処理の終了を依頼し、入れ替えの有無をローカル変数に保存する

            if (!isSwap)                                                                        // コマンド入れ替えが行われていないなら
            {
                maxArchiveCount--;                                                              // 記録数減算
                archiveIndex--;                                                                 // セーブ参照インデックスを減算
            }
            else　                                                                              // 入れ替えが実行されているなら
            {
                for (int i = 0;i < instanceGimmickController.Count;i++)                         // ギミック数分回す
                {
                    if (i == swappingGimmickIndex)                                              // 現在の入れ替えインデックスと同一のものなら
                    {
                        instanceGimmickController[i].AddControlCommandToArchive(archiveIndex);  // 書き換えられた管理コマンドをコピーしてアーカイブに登録する
                    }
                    else
                    {
                        instanceGimmickController[i].AddNewCommandsToArchive(archiveIndex);     // コマンドアーカイブに前回と同様の内容を追加する
                    }
                }

                maxArchiveCount = archiveIndex;                                                 // 記録数をセーブ参照インデックスと同様の値に変更
                storage.AddArchiveCommand(archiveIndex, storage.controlCommand);                // ストレージコマンドのアーカイブを追加する

                isExecute = false;
                state = CommandState.INACTIVE;

                foreach (GimmckController gimmck in instanceGimmickController)
                {
                    gimmck.IntializeAction();
                }
            }
        }

        public void StartCommandAction(Unit unit)
        {
            if (isExecute) return;
            isExecute = true;
            state = state != CommandState.MOVE_ON ? CommandState.MOVE_ON : CommandState.RETURN;

            foreach (GimmckController gimmckController in instanceGimmickController)
            {
                gimmckController.StartingAction(state);
            }
        }

        /// <summary>
        /// 一手戻る
        /// </summary>
        public void Undo(Unit unit)
        {
            if (archiveIndex <= 0 || isSwapping) return;     // セーブ参照インデックスが0よりも小さいか、入れ替え実行中であれば早期リターンする

            archiveIndex--;                                   // セーブ参照インデックスを減算する

            // 減算したセーブ情報に格納されていたコマンド情報を反映
            foreach (GimmckController gimmck in instanceGimmickController)
            {
                gimmck.IntializeAction();
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        /// <summary>
        /// 一手進む
        /// </summary>
        public void Redo(Unit unit)
        {
            if (archiveIndex >= maxArchiveCount - 1|| isSwapping) return;   // セーブ参照インデックスが要素数限界か、入れ替え実行中であれば早期リターンする

            archiveIndex++;                                                 // セーブ参照インデックスを加算する

            // 加算したセーブ情報に格納されていたコマンド情報を反映
            foreach (GimmckController gimmck in instanceGimmickController)
            {
                gimmck.IntializeAction();
                gimmck.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        int IGimmickAccess.GetAccessPointIndex(Vector3 position)
        {
            int retIndex = -1;                           // 返却インデックス
            float minDistance = AccessPoint.MAX_RADIUS;  // 計測した最短の距離

            for (int i = 0; i < accessPoints.Count; i++)
            {
                // y座標のない平面上の座標を作成
                Vector2 posA = new Vector2(position.x, position.z);
                Vector2 posB = new Vector2(accessPoints[i].transform.position.x, accessPoints[i].transform.position.z);

                float distance = Mathf.Abs(Vector2.Distance(posA, posB));   // x,z座標の距離を取得

                if (minDistance > distance)                                 // 最短距離よりも計測距離が短いなら
                {
                    minDistance = distance;                                 // 最短距離を更新
                    retIndex = i;                                           // 返却インデックスを更新
                }
            }

            return retIndex;
        }

        void IGimmickAccess.Access(int index)
        {
            if (isExecute) return;
            if (isSwapping) return;         // 入れ替え実行中であるなら早期リターンする
            isSwapping = true;              // 入れ替え実行中に変更

            swappingGimmickIndex = index;   // ギミック入れ替えインデックスを設定

            // コマンド管理クラスの入れ替え有効化関数を実行
            commandDirector.CommandActivation(instanceGimmickController[index].controlCommand);

            maxArchiveCount++;              // 記録数加算
            archiveIndex++;                 // セーブ参照インデックスを加算
        }
    }
}
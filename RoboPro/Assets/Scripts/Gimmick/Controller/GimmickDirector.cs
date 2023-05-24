using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UniRx;
using Command;
using Command.Entity;
using Gimmick.Interface;
using ScanMode;

namespace Gimmick
{
    /// <summary>
    /// ギミック関連の処理を管理するクラス
    /// </summary>
    public class GimmickDirector : MonoBehaviour, IGimmickAccess
    {
        [SerializeField, Tooltip("ストレージコマンド管理クラス")]
        private CommandStorage storage;

        [SerializeField, Tooltip("コマンド管理クラス")]
        private CommandDirector commandDirector;
       
        [SerializeField, Tooltip("使用するアクセスポイント")]
        private List<AccessPoint> accessPoints;

        [Inject]
        private IScanModeLaserManageable laserManageable;

        [Header("値確認用　数値変更非推奨")]
        [SerializeField]
        private int archiveIndex = 0;           // 現在のセーブ状況の何番目を実行しているかのインデックス(以下セーブ参照インデックスと呼ぶ)
        [SerializeField]
        private int swappingGimmickIndex = -1;  // 入れ替え中のギミックインデックス
        [SerializeField]
        private int maxArchiveCount = 0;      　// 記録している入れ替えの数

        private bool isSwapping = false;        // コマンド入れ替え実行中であるかを管理する変数 
        private bool isExecute = false;         // 実行可能であるか
        private CommandState state = CommandState.INACTIVE;

        private void Update()
        {
            if (isExecute)
            {
                if (!isSwapping)
                {
                    foreach (AccessPoint accessPoint in accessPoints)
                    {
                        accessPoint.controlGimmick.CommandUpdate();   // 各ギミックのコマンドを実行する
                    }

                    foreach (AccessPoint accessPoint in accessPoints)
                    {
                        if (accessPoint.controlGimmick.GetExecutionStatus) return;
                    }

                    isExecute = false;
                }
            }

            if (isSwapping && Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }

        /// <summary>
        /// ギミックインスタンス処理
        /// </summary>
        public void GimmickInstance()
        {
            List<ScanModeLaserTargetInfo> laserInfoList = new List<ScanModeLaserTargetInfo>();

            // 各要素に入れ替えの開始処理と終了処理を預け、生成インデックスを登録する
            for (int i = 0;i <  accessPoints.Count;i++)
            {
                laserInfoList.Add(new ScanModeLaserTargetInfo(accessPoints[i].transform,accessPoints[i].controlGimmick.transform,accessPoints[i].color));

                accessPoints[i].GimmickActivate();
            }

            laserManageable.LaserInit(laserInfoList);
        }

        /// <summary>
        /// コマンド入れ替え処理終了
        /// </summary>
        private void Close()
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
                for (int i = 0;i < accessPoints.Count;i++)                         // ギミック数分回す
                {
                    if (i == swappingGimmickIndex)                                              // 現在の入れ替えインデックスと同一のものなら
                    {
                        accessPoints[i].controlGimmick.AddControlCommandToArchive(archiveIndex);  // 書き換えられた管理コマンドをコピーしてアーカイブに登録する
                    }
                    else
                    {
                        accessPoints[i].controlGimmick.AddNewCommandsToArchive(archiveIndex);     // コマンドアーカイブに前回と同様の内容を追加する
                    }
                }

                maxArchiveCount = archiveIndex;                                                 // 記録数をセーブ参照インデックスと同様の値に変更
                storage.AddArchiveCommand(archiveIndex, storage.controlCommand);                // ストレージコマンドのアーカイブを追加する

                state = CommandState.INACTIVE;

                foreach (AccessPoint accessPoint in accessPoints)
                {
                    accessPoint.controlGimmick.IntializeAction();
                }
            }
        }

        public void StartCommandAction(Unit unit)
        {
            if (isExecute) return;
            isExecute = true;
            state = state != CommandState.MOVE_ON ? CommandState.MOVE_ON : CommandState.RETURN;

            foreach (AccessPoint accessPoint in accessPoints)
            {
                accessPoint.controlGimmick.StartingAction(state);
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
            foreach (AccessPoint accessPoint in accessPoints)
            {
                accessPoint.controlGimmick.IntializeAction();
                accessPoint.controlGimmick.OverwriteControlCommand(archiveIndex);
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
            foreach (AccessPoint accessPoint in accessPoints)
            {
                accessPoint.controlGimmick.IntializeAction();
                accessPoint.controlGimmick.OverwriteControlCommand(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        int IGimmickAccess.GetAccessPointIndex(Vector3 position)
        {
            int retIndex = -1;                           // 返却インデックス
            float minDistance = AccessPoint.RADIUS;  // 計測した最短の距離

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
            if (isSwapping) return;         // 入れ替え実行中であるなら早期リターンする
            isSwapping = true;              // 入れ替え実行中に変更

            swappingGimmickIndex = index;   // ギミック入れ替えインデックスを設定

            // コマンド管理クラスの入れ替え有効化関数を実行
            commandDirector.CommandActivation(accessPoints[index].controlGimmick.controlCommand);

            maxArchiveCount++;              // 記録数加算
            archiveIndex++;                 // セーブ参照インデックスを加算
        }
    }
}
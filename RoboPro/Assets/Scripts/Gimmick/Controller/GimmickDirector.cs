using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UniRx;
using Command;
using Command.Entity;
using Gimmick.Interface;
using InteractUI;
using ScanMode;
using Robo;

namespace Gimmick
{
    /// <summary>
    /// ギミック関連の処理を管理するクラス
    /// </summary>
    public class GimmickDirector : MonoBehaviour, IGimmickAccess
    {
        const float ACCESS_HEIGHT = 0.5f;

        [Inject]
        private IScanModeLaserManageable laserManageable;

        [Inject]
        private IInteractUIControllable uIControllable;

        [Inject]
        private IAudioPlayer audioPlayer;

        [SerializeField, Tooltip("ストレージコマンド管理クラス")]
        private CommandStorage storage;

        [SerializeField, Tooltip("コマンド管理クラス")]
        private CommandDirector commandDirector;

        [SerializeField]
        private DisplayInteractCanvasAsset displayInteract;

        private List<AccessPoint> accessPoints;

        [Header("値確認用　数値変更非推奨")]
        [SerializeField]
        private int archiveIndex = 0;           // 現在のセーブ状況の何番目を実行しているかのインデックス(以下セーブ参照インデックスと呼ぶ)
        [SerializeField]
        private int swappingGimmickIndex = -1;  // 入れ替え中のギミックインデックス
        [SerializeField]
        private int maxArchiveCount = 0;      　// 記録している入れ替えの数

        private int uiActive = -1;              // UIを表示しているか

        private bool isSwapping = false;        // コマンド入れ替え実行中であるかを管理する変数 
        private bool isExecute = false;         // 実行可能であるか
        private CommandState state = CommandState.INACTIVE;

        private Action undoPlayerAction;
        private Action redoPlayerAction;
        private Action savePlayerAction;

        private void Update()
        {
            if (isExecute)
            {
                if (!isSwapping)
                {
                    foreach (AccessPoint accessPoint in accessPoints)
                    {
                        accessPoint.ControlGimmicksUpdate();   // 各ギミックのコマンドを実行する
                    }

                    foreach (AccessPoint accessPoint in accessPoints)
                    {
                        // !
                        foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                        {
                            if (gimmick.GetExecutionStatus) return;
                        }
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
        public void GimmickInstance(Dictionary<BlockID,List<GameObject>> dic, List<AccessPointData> datas)
        {
            accessPoints = new List<AccessPoint>();
            List<ScanModeLaserTargetInfo> laserInfoList = new List<ScanModeLaserTargetInfo>();

            for (BlockID id = BlockID.Command_Red;id <= BlockID.Command_Black;id++)
            {
                if (dic.ContainsKey(id))
                {
                    accessPoints.Add(dic[id][0].GetComponent<AccessPoint>());
                    int index = datas.FindIndex(list => list.ColorID == (ColorID)((int)id - 100));
                    accessPoints[accessPoints.Count - 1].StartUp(datas[index]);

                    if (dic.ContainsKey(id + 100))
                    {
                        accessPoints[accessPoints.Count - 1].GimmickSubscrive(dic[id + 100]);
                    }
                    
                    if (dic.ContainsKey(id + 200))
                    {
                        accessPoints[accessPoints.Count - 1].GimmickSubscrive(dic[id + 200]);
                    }
                }
            }

            // 各要素に入れ替えの開始処理と終了処理を預け、生成インデックスを登録する
            for (int i = 0;i <  accessPoints.Count;i++)
            {
                // !
                foreach (GimmickController gimmick in accessPoints[i].controlGimmicks)
                {
                    laserInfoList.Add(new ScanModeLaserTargetInfo(accessPoints[i].transform, gimmick.transform, accessPoints[i].color));
                }
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

            audioPlayer.PlaySE(CueSheetType.System, "SE_System_Cancel");

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
                    accessPoints[i].ArchiveAdd(archiveIndex);
                }

                maxArchiveCount = archiveIndex;                                                 // 記録数をセーブ参照インデックスと同様の値に変更
                storage.AddArchiveCommand(archiveIndex, storage.controlCommand);                // ストレージコマンドのアーカイブを追加する

                savePlayerAction?.Invoke();

                state = CommandState.INACTIVE;

                foreach (AccessPoint accessPoint in accessPoints)
                {
                    // !
                    foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                    {
                        gimmick.IntializeAction();
                    }
                }
            }
        }

        public void StartCommandAction(Unit unit)
        {
            if (isExecute) return;
            isExecute = true;
            state = state != CommandState.MOVE_ON ? CommandState.MOVE_ON : CommandState.RETURN;

            audioPlayer.PlaySE(CueSheetType.System, "SE_System_PlayGimmick");

            foreach (AccessPoint accessPoint in accessPoints)
            {
                // !
                foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                {
                    gimmick.StartingAction(state);
                }
            }
        }

        /// <summary>
        /// 一手戻る
        /// </summary>
        public void Undo(Unit unit)
        {
            if (archiveIndex <= 0 || isSwapping) return;     // セーブ参照インデックスが1よりも小さいか、入れ替え実行中であれば早期リターンする

            archiveIndex--;                                   // セーブ参照インデックスを減算する

            audioPlayer.PlaySE(CueSheetType.System, "SE_System_UnDo");

            undoPlayerAction?.Invoke();

            // 減算したセーブ情報に格納されていたコマンド情報を反映
            foreach (AccessPoint accessPoint in accessPoints)
            {
                // !
                foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                {
                    gimmick.IntializeAction();
                }
                accessPoint.ArchiveSet(archiveIndex);
            }
            storage.OverwriteControlCommand(archiveIndex);
        }

        /// <summary>
        /// 一手進む
        /// </summary>
        public void Redo(Unit unit)
        {
            if (archiveIndex + 1 > maxArchiveCount|| isSwapping) return;   // セーブ参照インデックスが要素数限界か、入れ替え実行中であれば早期リターンする

            audioPlayer.PlaySE(CueSheetType.System, "SE_System_ReDo");

            archiveIndex++;                                                 // セーブ参照インデックスを加算する

            redoPlayerAction?.Invoke();

            // 加算したセーブ情報に格納されていたコマンド情報を反映
            foreach (AccessPoint accessPoint in accessPoints)
            {
                // !
                foreach (GimmickController gimmick in accessPoint.controlGimmicks)
                {
                    gimmick.IntializeAction();
                }
                accessPoint.ArchiveSet(archiveIndex);

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

                if (Mathf.Abs(position.y - accessPoints[i].transform.position.y) > ACCESS_HEIGHT) continue;

                float distance = Mathf.Abs(Vector2.Distance(posA, posB));   // x,z座標の距離を取得

                if (minDistance > distance)                                 // 最短距離よりも計測距離が短いなら
                {
                    minDistance = distance;                                 // 最短距離を更新
                    retIndex = i;                                           // 返却インデックスを更新
                }
            }

            if (uiActive != retIndex)
            {
                if (retIndex >= 0)
                {
                    uIControllable.HideUI();
                    uIControllable.HideLockUI();
                    uIControllable.ShowUI(ControllerType.Keyboard, displayInteract);
                    uIControllable.SetPosition(accessPoints[retIndex].transform.position + Vector3.up);
                }
                else
                {
                    uIControllable.HideUI();
                }

                uiActive = retIndex;
            }

            return retIndex;
        }

        bool IGimmickAccess.Access(int index)
        {
            if (isExecute) return false;
            if (isSwapping) return false;         // 入れ替え実行中であるなら早期リターンする
            isSwapping = true;              // 入れ替え実行中に変更

            swappingGimmickIndex = index;   // ギミック入れ替えインデックスを設定

            // コマンド管理クラスの入れ替え有効化関数を実行
            commandDirector.CommandActivation(accessPoints[index].controlCommands);

            maxArchiveCount++;              // 記録数加算
            archiveIndex++;                 // セーブ参照インデックスを加算

            uIControllable.HideUI();
            uiActive = -1;

            return true;
        }

        Vector3 IGimmickAccess.GetPosition(int index)
        {
            Vector3 retPosition = accessPoints[index].transform.position;
            return retPosition;
        }

        void IGimmickAccess.SetExecute(bool isExecute)
        {
            this.isExecute = isExecute;
        }

        void IGimmickAccess.SetAction(Action undoAct, Action redoAct, Action saveAct)
        {
            undoPlayerAction = undoAct;
            redoPlayerAction = redoAct;
            savePlayerAction = saveAct;
        }
    }
}
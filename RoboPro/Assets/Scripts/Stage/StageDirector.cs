using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using Gimmick;
using Command;

namespace Stage
{
    /// <summary>
    /// ステージ関連の処理を管理するクラス
    /// </summary>
    public class StageDirector : MonoBehaviour
    {
        [SerializeField, Tooltip("ギミック管理クラス")]
        private GimmickDirector gimmickDirector;

        [SerializeField, Tooltip("ステージが扱うのボタン管理クラス")]
        private StageUIManager uiManager;

        // Start is called before the first frame update
        void Awake()
        {
            Dictionary<BlockID, List<GameObject>> obj = new Dictionary<BlockID, List<GameObject>>();
            List<AccessPointData> datas = new List<AccessPointData>();

            StageDataCreater stageDataCreater = GetComponent<StageDataCreater>();
            stageDataCreater.StageCreate(obj,ref datas);

            gimmickDirector.GimmickInstance(obj, datas);  // ギミック管理クラスにギミック生成を依頼

            Subject<Unit> undo = new Subject<Unit>();
            undo.Subscribe(gimmickDirector.Undo);
            uiManager.undo = undo;

            Subject<Unit> redo = new Subject<Unit>();
            redo.Subscribe(gimmickDirector.Redo);
            uiManager.redo = redo;

            Subject<Unit> play = new Subject<Unit>();
            play.Subscribe(gimmickDirector.StartCommandAction);
            uiManager.play = play;
        }
    }

}
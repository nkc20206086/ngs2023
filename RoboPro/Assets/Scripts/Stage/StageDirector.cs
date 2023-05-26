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
    /// ステージ関連の処理を管理するクラス(現在は特に記述することがない)
    /// </summary>
    public class StageDirector : MonoBehaviour
    {
        [SerializeField, Tooltip("ギミック管理クラス")]
        private GimmickDirector gimmickDirector;

        [SerializeField, Tooltip("ステージが扱うのボタン管理クラス")]
        private StageUIManager uiManager;

        [SerializeField]
        private TextAsset stageData;

        // Start is called before the first frame update
        void Start()
        {
            StageDataCreater stageDataCreater = GetComponent<StageDataCreater>();
            stageDataCreater.StageCreate();

            gimmickDirector.GimmickInstance();  // ギミック管理クラスにギミック生成を依頼

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
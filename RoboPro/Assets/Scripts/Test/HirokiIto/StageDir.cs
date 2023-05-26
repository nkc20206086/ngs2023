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
    public class StageDir : MonoBehaviour
    {
        [SerializeField, Tooltip("ギミック管理クラス")]
        private GimDir gimmickDirector;

        [SerializeField, Tooltip("ステージが扱うのボタン管理クラス")]
        private StageUIManager uiManager;

        // Start is called before the first frame update
        void Start()
        {
            List<CommandContainer[]> setCommands = new List<CommandContainer[]>();

            for (int i = 0; i < 2; i++)
            {
                // 初期コマンド設定
                setCommands.Add(new CommandContainer[4]);
                setCommands[i][0] = new CommandContainer(MainCommandType.Scale, false, false, false, -30, CoordinateAxis.X, 1);
                setCommands[i][1] = new CommandContainer(MainCommandType.Scale, false, false, false, 10, CoordinateAxis.Y, 1);
                setCommands[i][2] = new CommandContainer(MainCommandType.Scale, false, false, false, -10, CoordinateAxis.X, 1);
                setCommands[i][3] = new CommandContainer(MainCommandType.None, false, false, false, default, default, default);
            }

            gimmickDirector.GimmickInstance(setCommands);  // ギミック管理クラスにギミック生成を依頼

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
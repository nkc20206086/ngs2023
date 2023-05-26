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
    /// �X�e�[�W�֘A�̏������Ǘ�����N���X(���݂͓��ɋL�q���邱�Ƃ��Ȃ�)
    /// </summary>
    public class StageDirector : MonoBehaviour
    {
        [SerializeField, Tooltip("�M�~�b�N�Ǘ��N���X")]
        private GimmickDirector gimmickDirector;

        [SerializeField, Tooltip("�X�e�[�W�������̃{�^���Ǘ��N���X")]
        private StageUIManager uiManager;

        [SerializeField]
        private TextAsset stageData;

        // Start is called before the first frame update
        void Start()
        {
            StageDataCreater stageDataCreater = GetComponent<StageDataCreater>();
            stageDataCreater.StageCreate();

            gimmickDirector.GimmickInstance();  // �M�~�b�N�Ǘ��N���X�ɃM�~�b�N�������˗�

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
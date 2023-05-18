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

        // Start is called before the first frame update
        void Start()
        {
            List<CommandStruct[]> setCommands = new List<CommandStruct[]>();

            for (int i = 0;i < 2;i++)
            {
                // �����R�}���h�ݒ�
                setCommands.Add(new CommandStruct[4]);
                setCommands[i][0] = new CommandStruct(MainCommandType.Scale,false,false,false,-30,CoordinateAxis.X,1);
                setCommands[i][1] = new CommandStruct(MainCommandType.Scale,false,false,false,10,CoordinateAxis.Y,1);
                setCommands[i][2] = new CommandStruct(MainCommandType.Scale,false,false,false,-10,CoordinateAxis.X,1);
                setCommands[i][3] = new CommandStruct(MainCommandType.None,false,false,false,default,default,default);
            }

            gimmickDirector.GimmickInstance(setCommands);  // �M�~�b�N�Ǘ��N���X�ɃM�~�b�N�������˗�

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
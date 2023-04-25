using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmick;
using Command;

namespace Stage
{
    /// <summary>
    /// �X�e�[�W�֘A�̏������Ǘ�����N���X(���݂͓��ɋL�q���邱�Ƃ��Ȃ�)
    /// </summary>
    public class StageDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("�M�~�b�N�Ǘ��N���X")]
        private GimmickDirector gimmickDirector;

        // Start is called before the first frame update
        void Start()
        {
            List<CommandStruct[]> setCommands = new List<CommandStruct[]>();

            for (int i = 0;i < 2;i++)
            {
                // �����R�}���h�ݒ�
                setCommands.Add(new CommandStruct[3]);
                setCommands[i][0] = new CommandStruct(MainCommandType.Move,false,false,false,30,CoordinateAxis.X,1);
                setCommands[i][1] = new CommandStruct(MainCommandType.Rotate,false,false,false,90,CoordinateAxis.Z,1);
                setCommands[i][2] = new CommandStruct(MainCommandType.Move,false,false,false,30,CoordinateAxis.Y,1);
            }

            gimmickDirector.GimmickInstance(setCommands);  // �M�~�b�N�Ǘ��N���X�ɃM�~�b�N�������˗�
        }

        // Update is called once per frame
        void Update()
        {
            // �����̏����̓f�o�b�O�p�Ȃ̂ŁA���ۂɗp����ꍇ�͕ύX���邱��

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gimmickDirector.Undo();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gimmickDirector.Redo();
            }
        }
    }

}
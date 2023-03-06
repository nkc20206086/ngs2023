using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmick;

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
            gimmickDirector.GimmickInstance();  // �M�~�b�N�Ǘ��N���X�ɃM�~�b�N�������˗�
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
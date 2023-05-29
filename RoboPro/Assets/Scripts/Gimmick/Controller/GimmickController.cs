using UnityEngine;
using System;
using Command;
using Command.Entity;
using System.Collections.Generic;

namespace Gimmick
{
    // ���s�R�}���h�ƊǗ��R�}���h��������Ă���݌v�ɂȂ��Ă���
    // �����Undo��Redo�ɂ��l�̓���ւ����������ہA���ۂɓ������R�}���h�ƊǗ����Ă���R�}���h���قȂ�\�������邽��

    /// <summary>
    /// �M�~�b�N�Ǘ��N���X
    /// </summary>
    public class GimmickController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[CommandUtility.commandCount]; // �Ǘ��R�}���h�̔z��

        [SerializeField]
        private int playIndex = -1;                                                         // ���s����Ă���R�}���h�̃C���f�b�N�X
        private float timeCount = 0;                                                        // ���Ԍv���p�ϐ�
        public MainCommand[] playCommand = new MainCommand[CommandUtility.commandCount];   // ���s�R�}���h�̔z��
        private List<GimmickArchive> gimmickArchives = new List<GimmickArchive>();          // �L�^�p�\���̂̃��X�g

        // �e���s���_�ł̌��_
        private Vector3 basePos;
        private Quaternion baseQuat;
        private Vector3 baseScale;

        [Header("�l�m�F�p ���l�ύX�񐄏�")]
        [SerializeField]
        private bool isExecutable = false;                                                  // ���s�\��
        [SerializeField]
        private CommandState state;                                                         // �������̏�ԕϐ�

        public bool GetExecutionStatus { get => isExecutable; }

        private void Start()
        {
            state = CommandState.MOVE_ON;                                   // �R�}���h�X�e�[�g��ʏ�ړ��ɂ���

            basePos = transform.position;
            baseQuat = transform.rotation;
            baseScale = transform.localScale;
        }

        /// <summary>
        /// �R�}���h�̎��s�֐�
        /// </summary>
        public void CommandUpdate()
        {
            if (!isExecutable) return;          // ���s�s�ł���Α������^�[������

            if (playCommand.Length >= CommandUtility.specialCommandNumber)
            {
                SpecialCommandUpdate();
            }

            if (timeCount > 0)                  // ���Ԍv���p�ϐ���0�ȏ�̐��l�����Ȃ�
            {
                timeCount -= Time.deltaTime;    // ���Ԍv���p�ϐ������炷
                if (timeCount < 0)              // ����ɂ��0�����ɂȂ����ꍇ
                {
                    timeCount = 0.0f;           // �v���p�ϐ�������������
                    IndexSwitching();           // ���s�C���f�b�N�X�ύX�p�֐�
                }
            }
            else�@                              // ���Ԍv���p�ϐ����l�������Ȃ��ꍇ
            {
                // ���݂̎��s�C���f�b�N�X�̃R�}���h�����s
                playCommand[playIndex].CommandExecute(state, transform);
            }
        }

        public void StartingAction(CommandState playState)
        {
            if (playState == CommandState.MOVE_ON)
            {
                CommandCalc();
                isExecutable = true;
                playIndex = 0;
            }
            else if (playState == CommandState.RETURN)
            {
                if (state != CommandState.MOVE_ON) return;
                isExecutable = true;
                playIndex = playCommand.Length - 1;
            }

            state = playState;

            if (playCommand[playIndex] == null ||
                playCommand[playIndex].GetMainCommandType() == MainCommandType.None ||
                !playCommand[playIndex].CommandNullCheck())
            {
                IndexSwitching();
            }
        }

        public void IntializeAction()
        {
            state = CommandState.INACTIVE;
            isExecutable = false;
            playIndex = 0;
            transform.position = basePos;
            transform.rotation = baseQuat;
            transform.localScale = baseScale;
        }

        public void CommandSet(MainCommand[] baseArray)
        {
            MainCommand[] newArray = new MainCommand[baseArray.Length];

            for (int i = 0;i < baseArray.Length;i++)
            {
                newArray[i] = baseArray[i] != null ? baseArray[i].MainCommandClone() : null;
            }

            playCommand = newArray;
        }

        /// <summary>
        /// ���s�C���f�b�N�X�ύX�p�֐�
        /// </summary>
        private void IndexSwitching()
        {
            if (state == CommandState.MOVE_ON)              // �ʏ�ړ��ł���΃C���f�b�N�X�����Z
            {
                playIndex++;
                if (playCommand.Length <= playIndex)
                {
                    playIndex = playCommand.Length - 1;
                    isExecutable = false;
                }
                else if (playCommand[playIndex] == null ||
                         playCommand[playIndex].GetMainCommandType() == MainCommandType.None || 
                         !playCommand[playIndex].CommandNullCheck())
                {
                    IndexSwitching();
                }
            }
            else                                            // ���]�ړ��ł���΃C���f�b�N�X�����Z
            {
                playIndex--;
                if (playIndex < 0)
                {
                    playIndex = 0;
                    isExecutable = false;
                }
                else if (playCommand[playIndex] == null ||
                         playCommand[playIndex].GetMainCommandType() == MainCommandType.None ||
                         !playCommand[playIndex].CommandNullCheck())
                {
                    IndexSwitching();
                }
            }
        }

        /// <summary>
        /// �R�}���h�I�����ɃC���^�[�o����݂��邽�߂̏���
        /// </summary>
        private void CreateInterval()
        {
            timeCount = 1.0f;
        }

        /// <summary>
        /// SP�R�}���h�ɂ����ʂ̊֐�
        /// </summary>
        protected virtual void SpecialCommandUpdate() 
        {
            if (playCommand[3] == null) return;
            if (playCommand[3].GetMainCommandType() == MainCommandType.Move)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
        }

        private void CommandCalc()
        {
            Vector3 move = Vector3.zero;
            Quaternion rotation = baseQuat;
            Vector3 scale = baseScale;

            // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
            for (int i = 0; i < playCommand.Length; i++)
            {
                if (playCommand[i] == null ||
                    !playCommand[i].CommandNullCheck()) continue;

                switch (playCommand[i]?.GetMainCommandType())
                {
                    case MainCommandType.Move:
                        Vector3 moveValue = (Vector3)playCommand[i].InitCommand(basePos + move, CreateInterval);
                        move += moveValue;
                        break;
                    case MainCommandType.Rotate:
                        Quaternion rotateValue = (Quaternion)playCommand[i].InitCommand(rotation, CreateInterval);
                        rotation = rotateValue;
                        break;
                    case MainCommandType.Scale:
                        Vector3 scaleValue = (Vector3)playCommand[i].InitCommand(scale, CreateInterval);
                        scale = scaleValue;
                        break;
                }
            }
        }
    }
}
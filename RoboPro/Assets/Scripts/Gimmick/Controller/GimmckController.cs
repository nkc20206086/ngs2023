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
    public class GimmckController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[CommandUtility.commandCount]; // �Ǘ��R�}���h�̔z��

        [SerializeField]
        private int playIndex = -1;                                                         // ���s����Ă���R�}���h�̃C���f�b�N�X
        private float timeCount = 0;                                                        // ���Ԍv���p�ϐ�
        private MainCommand[] playCommand = new MainCommand[CommandUtility.commandCount];   // ���s�R�}���h�̔z��
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

        /// <summary>
        /// �M�~�b�N�̊J�n���֐�
        /// </summary>
        /// <param name="setCommands">���̃M�~�b�N�̎��R�}���h</param>
        public void StartUp(CommandContainer[] setCommands)
        {
            controlCommand = new MainCommand[setCommands.Length];           // �Ǘ��R�}���h������
            playCommand = new MainCommand[setCommands.Length];

            for (int i = 0;i < setCommands.Length;i++)
            {
                // �\���̂����C���R�}���h�N���X�ɕϊ����A�Ǘ��R�}���h�ɐݒ肷��
                controlCommand[i] = CommandCreater.CreateCommand(setCommands[i]);
            }

            state = CommandState.MOVE_ON;                                   // �R�}���h�X�e�[�g��ʏ�ړ��ɂ���

            Array.Copy(controlCommand, playCommand, controlCommand.Length); // �Ǘ��R�}���h�Ɏ��s�R�}���h�̓��e���R�s�[

            IndexSwitching();                                               // ���s�C���f�b�N�X�ύX�p�֐�

            basePos = transform.position;
            baseQuat = transform.rotation;
            baseScale = transform.localScale;

            Vector3 move = Vector3.zero;
            Quaternion rotation = baseQuat;
            Vector3 scale = baseScale;

            // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
            for (int i = 0; i < CommandUtility.commandCount; i++)
            {
                switch (playCommand[i].GetMainCommandType())
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

            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand,playCommand,playIndex);
            gimmickArchives.Add(gimmickArchive);
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

        /// <summary>
        /// �w��C���f�b�N�X�̃A�[�J�C�u�ɃR�}���h���X�g��ǉ�����
        /// </summary>
        /// <param name="index">�o�^�C���f�b�N�X</param>
        public void AddNewCommandsToArchive(int index)
        {
            if (index > gimmickArchives.Count) return;                      // �o�^�C���f�b�N�X���v�f���𒴂��Ă���Ȃ瑁�����^�[������

            if (gimmickArchives.Count != index)                             // �v�f���Ɠo�^�C���f�b�N�X���قȂ�Ȃ�(�����Ă���ꍇ�͑������^�[�������̂ŁA�����I�ɓ������̂łȂ��ꍇ)
            {
                for (int i = gimmickArchives.Count - 1; i >= index; i--)    
                {
                    // �����Ă���v�f�����ׂă��X�g����폜����
                    gimmickArchives.RemoveAt(i);
                }
            }

            // �L�^�p�̓��e���\���̂ɂ܂Ƃ߂�
            GimmickArchive gimmickArchive = new GimmickArchive(gimmickArchives[gimmickArchives.Count - 1].controlCommand, playCommand, playIndex);
            gimmickArchives.Add(gimmickArchive);                            // �L�^�����X�g�ɒǉ�����
        }

        /// <summary>
        /// ���݊Ǘ����Ă���R�}���h���R�}���h�A�[�J�C�u�ɓo�^����
        /// </summary>
        /// <param name="index">�o�^�C���f�b�N�X</param>
        public void AddControlCommandToArchive(int index)
        {
            if (index > gimmickArchives.Count) return;                      // �o�^�C���f�b�N�X���v�f���𒴂��Ă���Ȃ瑁�����^�[������

            if (gimmickArchives.Count != index)                             // �v�f���Ɠo�^�C���f�b�N�X���قȂ�Ȃ�(�����Ă���ꍇ�͑������^�[�������̂ŁA�����I�ɓ������̂łȂ��ꍇ)
            {
                for (int i = gimmickArchives.Count - 1; i >= index; i--)
                {
                    // �����Ă���v�f�����ׂă��X�g����폜����
                    gimmickArchives.RemoveAt(i);
                }
            }

            // �L�^�p�̓��e���\���̂ɂ܂Ƃ߂�
            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand, playCommand, playIndex);
            gimmickArchives.Add(gimmickArchive);                            // �L�^�����X�g�ɒǉ�����
        }

        /// <summary>
        /// �w��C���f�b�N�X�̃R�}���h�A�[�J�C�u���e���Ǘ��R�}���h�ɔ��f����֐�
        /// </summary>
        /// <param name="index">�Ώۂ̃C���f�b�N�X</param>
        public void OverwriteControlCommand(int index)
        {
            gimmickArchives[index].SetGimmickArchive(controlCommand,playCommand,playIndex);

            timeCount = 0.0f;

            Vector3 move = Vector3.zero;
            Quaternion rotation = baseQuat;
            Vector3 scale = baseScale;

            // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
            for (int i = 0; i < CommandUtility.commandCount; i++)
            {
                switch (playCommand[i].GetMainCommandType())
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

        public void StartingAction(CommandState playState)
        {
            if (playState == CommandState.MOVE_ON)
            {
                CheckExecutable();

                if (!isExecutable) return;
            }
            else if (playState == CommandState.RETURN)
            {
                if (state != CommandState.MOVE_ON) return;
                isExecutable = true;
                playIndex = CommandUtility.commandCount - 1;
            }

            state = playState;
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

        /// <summary>
        /// ���s�C���f�b�N�X�ύX�p�֐�
        /// </summary>
        private void IndexSwitching()
        {
            if (state == CommandState.MOVE_ON)              // �ʏ�ړ��ł���΃C���f�b�N�X�����Z
            {
                playIndex++;
                if (CommandUtility.commandCount <= playIndex)
                {
                    playIndex = CommandUtility.commandCount - 1;
                    isExecutable = false;
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
        /// ���s�\�ł��邩���m�F����֐�
        /// </summary>
        private void CheckExecutable()
        {
            if (controlCommand.Length <= 0)                                     // �R�}���h�Ǘ�����0�������Ȃ�
            {
                isExecutable = false;                                           // ���s�s�ɕύX
                playCommand = new MainCommand[controlCommand.Length];           // ���s�R�}���h�̒��g����ɂ���
                return;
            }
            else�@                                   
            {

                // �Ǘ��R�}���h�̗v�f�Ɏ��s�s�ł�����̂��܂܂��΁A���s�s�ɕύX���������^�[������
                for (int i = 0;i < CommandUtility.commandCount;i++)
                {
                    if (controlCommand[i] == null || !controlCommand[i].CommandNullCheck())
                    {
                        isExecutable = false;
                        playCommand = new MainCommand[controlCommand.Length];
                        return;
                    }
                }

                isExecutable = true;                                            // ���s�\�ɕύX
                Array.Copy(controlCommand, playCommand, controlCommand.Length); // �Ǘ��R�}���h�Ɏ��s�R�}���h�̓��e���R�s�[

                Vector3 move = Vector3.zero;
                Quaternion rotation = baseQuat;
                Vector3 scale = baseScale;

                // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
                for (int i = 0; i < CommandUtility.commandCount; i++)
                {
                    switch (playCommand[i].GetMainCommandType())
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
    }
}
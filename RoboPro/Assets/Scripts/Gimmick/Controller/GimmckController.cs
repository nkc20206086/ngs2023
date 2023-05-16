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
        public MainCommand[] controlCommand = new MainCommand[3];                       // �Ǘ��R�}���h�̔z��

        private int playIndex = -1;                                                     // ���s����Ă���R�}���h�̃C���f�b�N�X
        private float timeCount = 0;                                                    // ���Ԍv���p�ϐ�
        private MainCommand[] playCommand = new MainCommand[3];                         // ���s�R�}���h�̔z��
        private List<GimmickArchive> gimmickArchives = new List<GimmickArchive>();      // �L�^�p�\���̂̃��X�g

        // �e���s���_�ł̌��_
        private Vector3 basePos;
        private Quaternion baseQuat;
        private Vector3 baseScale;

        [Header("�l�m�F�p ���l�ύX�񐄏�")]
        [SerializeField]
        private bool isExecutable = true;                                       // ���s�\��
        [SerializeField]
        private CommandState state;                                             // �������̏�ԕϐ�

        /// <summary>
        /// �M�~�b�N�̊J�n���֐�
        /// </summary>
        /// <param name="setCommands">���̃M�~�b�N�̎��R�}���h</param>
        public void StartUp(CommandStruct[] setCommands)
        {
            controlCommand = new MainCommand[setCommands.Length];           // �Ǘ��R�}���h������

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
            foreach (MainCommand command in playCommand)
            {
                switch (command.GetMainCommandType())
                {
                    case MainCommandType.Move:
                        Vector3 moveValue = (Vector3)command.StartUp(basePos + move, CreateInterval);
                        move += moveValue;
                        break;
                    case MainCommandType.Rotate:
                        Quaternion rotateValue = (Quaternion)command.StartUp(rotation, CreateInterval);
                        rotation = rotateValue;
                        break;
                }
            }

            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand,playCommand,transform,state,playIndex);
            gimmickArchives.Add(gimmickArchive);
        }

        /// <summary>
        /// �R�}���h�̎��s�֐�
        /// </summary>
        public void CommandUpdate()
        {
            if (!isExecutable)                  // ���s����Ă��Ȃ��Ȃ���s�\�����`�F�b�N����
            {
                CheckExecutable();
                return;
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
            GimmickArchive gimmickArchive = new GimmickArchive(gimmickArchives[gimmickArchives.Count - 1].controlCommand, playCommand, transform, state, playIndex);
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
            GimmickArchive gimmickArchive = new GimmickArchive(controlCommand, playCommand, transform, state, playIndex);
            gimmickArchives.Add(gimmickArchive);                            // �L�^�����X�g�ɒǉ�����
        }

        /// <summary>
        /// �w��C���f�b�N�X�̃R�}���h�A�[�J�C�u���e���Ǘ��R�}���h�ɔ��f����֐�
        /// </summary>
        /// <param name="index">�Ώۂ̃C���f�b�N�X</param>
        public void OverwriteControlCommand(int index)
        {
            gimmickArchives[index].SetGimmickArchive(controlCommand,playCommand,transform,out state,out playIndex);

            timeCount = 0.0f;

            Vector3 move = Vector3.zero;
            Quaternion rotation = baseQuat;
            Vector3 scale = baseScale;
            // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
            foreach (MainCommand command in playCommand)
            {
                switch (command.GetMainCommandType())
                {
                    case MainCommandType.Move:
                        Vector3 moveValue = (Vector3)command.StartUp(basePos + move, CreateInterval);
                        move += moveValue;
                        break;
                    case MainCommandType.Rotate:
                        Quaternion rotateValue = (Quaternion)command.StartUp(rotation, CreateInterval);
                        rotation = rotateValue;
                        break;
                }
            }
        }

        /// <summary>
        /// ���s�C���f�b�N�X�ύX�p�֐�
        /// </summary>
        private void IndexSwitching()
        {
            if (state == CommandState.MOVE_ON)      // �ʏ�ړ��ł���΃C���f�b�N�X�����Z
            {
                playIndex++;
            }
            else                                    // ���]�ړ��ł���΃C���f�b�N�X�����Z
            {
                playIndex--;
            }

            if (playCommand.Length <= playIndex)    // ���s�C���f�b�N�X���Ǘ��R�}���h�̐��𒴂�����              
            {
                state = CommandState.RETURN;        // �R�}���h�X�e�[�g�𔽓]�ړ��ɂ���
                playIndex--;                        // ���s�C���f�b�N�X�����Z����
            }
            else if (playIndex < 0)                 // ���s�C���f�b�N�X��0�����ł���Ȃ�
            {
                playIndex = 0;                      // ���s�C���f�b�N�X��0�ɐݒ�
                state = CommandState.MOVE_ON;       // �R�}���h�X�e�[�g��ʏ�ړ��ɕύX
                CheckExecutable();                  // ���s�\�ł��邩���m�F 
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
            if (controlCommand.Length <= 0)         // �R�}���h�Ǘ�����0�������Ȃ�
            {
                isExecutable = false;               // ���s�s�ɕύX
                playCommand = new MainCommand[3];   // ���s�R�}���h�̒��g����ɂ���
                return;
            }
            else�@                                   
            {
                // �Ǘ��R�}���h�̗v�f�Ɏ��s�s�ł�����̂��܂܂��΁A���s�s�ɕύX���������^�[������
                foreach (MainCommand command in controlCommand)
                {
                    if (command == null || !command.CommandNullCheck())
                    {
                        isExecutable = false;
                        playCommand = new MainCommand[3];
                        return;
                    }
                }

                isExecutable = true;                                            // ���s�\�ɕύX
                Array.Copy(controlCommand, playCommand, controlCommand.Length); // �Ǘ��R�}���h�Ɏ��s�R�}���h�̓��e���R�s�[

                Vector3 move = Vector3.zero;
                Quaternion rotation = baseQuat;
                Vector3 scale = baseScale;
                // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
                foreach (MainCommand command in playCommand)
                {
                    switch (command.GetMainCommandType())
                    {
                        case MainCommandType.Move:
                            Vector3 moveValue = (Vector3)command.StartUp(basePos + move,CreateInterval);
                            move += moveValue;
                            break;
                        case MainCommandType.Rotate:
                            Quaternion rotateValue = (Quaternion)command.StartUp(rotation,CreateInterval);
                            rotation = rotateValue;
                            break;
                    }
                }
            }
        }
    }
}
using UnityEngine;
using System;
using Command;
using Command.Entity;
using System.Collections.Generic;

namespace Gimmick
{
    /// <summary>
    /// �M�~�b�N�Ǘ��N���X
    /// </summary>
    public class GimmckController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[3];               // ���̃M�~�b�N�̎����C���R�}���h�z��

        private int playIndex = -1;                                             // ���̃M�~�b�N�̐����C���f�b�N�X
        private float timeCount = 0;                                            // ���Ԍv���p�ϐ�
        private MainCommand[] playCommand = new MainCommand[3];                 // ���s�R�}���h�̔z��
        private List<MainCommand[]> commandArchive = new List<MainCommand[]>(); // �ύX��ۑ����Ă����R�}���h�A�[�J�C�u

        [Header("�l�m�F�p ���l�ύX�񐄏�")]
        [SerializeField]
        private bool isExecutable = true;                                       // ���s�\��
        [SerializeField]
        private CommandState state;                                             // �������̏�ԕϐ�

        // Start is called before the first frame update
        void Start()
        {
            // �����l��ݒ�(�����_�łǂ̂悤�ȃR�}���h��ݒ肷��@�\�����Ă��Ȃ�����)
            CommandStruct st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.X, 1);
            controlCommand[0] = CommandCreater.CreateCommand(st);
            st = new CommandStruct(MainCommandType.Rotate, false, false, false, 90, CoordinateAxis.Z, 1);
            controlCommand[1] = CommandCreater.CreateCommand(st);
            st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.Y, 1);
            controlCommand[2] = CommandCreater.CreateCommand(st);

            state = CommandState.MOVE_ON;                                   // �R�}���h�X�e�[�g��ʏ�ړ��ɂ���

            Array.Copy(controlCommand, playCommand, controlCommand.Length); // �Ǘ��R�}���h�Ɏ��s�R�}���h�̓��e���R�s�[

            IndexSwitching();                                               // ���s�C���f�b�N�X�ύX�p�֐�

            // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
            foreach (MainCommand command in playCommand)                    
            {
                command.StartUp();
            }

            MainCommand[] copyCommand = new MainCommand[3];
            for (int i = 0;i < copyCommand.Length;i++)
            {
                copyCommand[i] = controlCommand[i].MainCommandClone();
            }

            commandArchive.Add(copyCommand);                             // �����R�}���h���A�[�J�C�u�ɒǉ�����
        }

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
            if (index > commandArchive.Count) return;                           // �v�f���𒴂��Ă���Ȃ�ǉ��ł��Ȃ����ߑ������^�[������

            MainCommand[] copyArray = new MainCommand[controlCommand.Length];   // ���X�g�ɒǉ�����p�̃��[�J���z����쐬

            for (int i = 0;i < commandArchive[commandArchive.Count - 1].Length;i++)
            {
                // �����v�f�ԍ��̗v�f���R�s�[����(�l�����݂��Ȃ��Ȃ�f�t�H���g�l���쐬)
                copyArray[i] = commandArchive[commandArchive.Count - 1][i] == null ? default : commandArchive[commandArchive.Count - 1][i].MainCommandClone(); 
            }

            if (commandArchive.Count != index)                                  // �v�f�ԍ��ƃR�}���h�A�[�J�C�u�̗v�f�����قȂ�Ȃ�(�v�f�����傫�����l�͑������^�[������邽�ߍő吔���ۂ��̔��ʂƂȂ�)
            {
                for (int i = commandArchive.Count - 1;i >= index;i--)
                {
                    // �]�蕪�̗v�f�����ׂĔj������
                    commandArchive.RemoveAt(i);                                 
                }
            }

            commandArchive.Add(copyArray);                                      // �R�s�[�����z����R�}���h�A�[�J�C�u�ɉ�����
        }

        /// <summary>
        /// ���݊Ǘ����Ă���R�}���h���R�}���h�A�[�J�C�u�ɓo�^����
        /// </summary>
        /// <param name="index">�o�^�C���f�b�N�X</param>
        public void AddControlCommandToArchive(int index)
        {
            if (index > commandArchive.Count) return;                           // �v�f���𒴂��Ă���Ȃ�ǉ��ł��Ȃ����ߑ������^�[������

            MainCommand[] copyArray = new MainCommand[controlCommand.Length];   // ���X�g�ɒǉ�����p�̃��[�J���̔z����쐬

            for (int i = 0; i < controlCommand.Length; i++)
            {
                // �����v�f�ԍ��̗v�f���R�s�[����(�l�����݂��Ȃ��Ȃ�f�t�H���g�l���쐬)
                copyArray[i] = controlCommand[i] == null ? default : controlCommand[i].MainCommandClone();
            }

            if (commandArchive.Count != index)                                  // �v�f�ԍ��ƍő吔���قȂ�Ȃ�
            {
                for (int i = commandArchive.Count - 1; i >= index; i--)
                {
                    // �]�蕪�̗v�f�����ׂĔj������
                    commandArchive.RemoveAt(i);
                }
            }

            commandArchive.Add(copyArray);                                      // �R�s�[�����z����R�}���h�A�[�J�C�u�ɉ�����
        }

        /// <summary>
        /// �w��C���f�b�N�X�̃R�}���h�A�[�J�C�u���e���Ǘ��R�}���h�ɔ��f����֐�
        /// </summary>
        /// <param name="index">�Ώۂ̃C���f�b�N�X</param>
        public void OverwriteControlCommand(int index)
        {
            MainCommand[] copyArray = new MainCommand[controlCommand.Length];   // �Ǘ��R�}���h���������p�̃��[�J���z����쐬����

            for (int i = 0; i < commandArchive[index].Length; i++)
            {
                // ���������p�z��ɃR�}���h�A�[�J�C�u��[�w��C���f�b�N�X][�v�f�ԍ�]�̓��e���R�s�[���ē���Ă���(�l�����݂��Ȃ��Ȃ�f�t�H���g�l��ݒ�)
                copyArray[i] = commandArchive[index][i] == null ? default : commandArchive[index][i].MainCommandClone(); 
            }

            controlCommand = copyArray;                                         // �Ǘ��R�}���h�����������p�z��ɕύX
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

            // �Ώۂ̃R�}���h�̗L�����������s��
            playCommand[playIndex]?.ActionActivate(CreateInterval, gameObject);
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

                // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
                foreach (MainCommand command in playCommand)
                {
                    command.StartUp();
                }

                playCommand[0]?.ActionActivate(CreateInterval, gameObject);     // �擪�v�f�ɗL�����֐������s
            }
        }
    }
}
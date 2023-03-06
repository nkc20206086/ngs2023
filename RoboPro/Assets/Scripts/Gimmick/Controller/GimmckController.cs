using UnityEngine;
using System;
using Command;
using Command.Entity;

namespace Gimmick
{
    /// <summary>
    /// �M�~�b�N�Ǘ��N���X
    /// </summary>
    public class GimmckController : MonoBehaviour
    {
        public MainCommand[] controlCommand = new MainCommand[3];   // ���̃M�~�b�N�̎����C���R�}���h�z��

        private int index = -1;                                     // ���̃M�~�b�N�̐����C���f�b�N�X
        private float count = 0;                                    // ���Ԍv���p�ϐ�
        private MainCommand[] playCommand = new MainCommand[3];     // ���s�R�}���h�̔z��

        [Header("�l�m�F�p ���l�ύX�񐄏�")]
        [SerializeField]
        private bool play = true;

        [SerializeField]
        private CommandState state;


        // Start is called before the first frame update
        void Start()
        {
            // �����l��ݒ�(�����_�łǂ̂悤�ȃR�}���h��ݒ肷��@�\�����Ă��Ȃ�����)
            CommandStruct st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.X, 1);
            controlCommand[0] = (CommandCreater.CreateCommand(st));
            st = new CommandStruct(MainCommandType.Rotate, false, false, false, 90, CoordinateAxis.Z, 1);
            controlCommand[1] = (CommandCreater.CreateCommand(st));
            st = new CommandStruct(MainCommandType.Move, false, false, false, 30, CoordinateAxis.Y, 1);

            controlCommand[2] = (CommandCreater.CreateCommand(st));
            state = CommandState.MOVE_ON;                                   // �R�}���h�X�e�[�g��ʏ�ړ��ɂ���

            Array.Copy(controlCommand, playCommand, controlCommand.Length); // �Ǘ��R�}���h�Ɏ��s�R�}���h�̓��e���R�s�[

            OnExecute();                                                    // ���s�C���f�b�N�X�ύX�p�֐�

            // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
            foreach (MainCommand command in playCommand)                    
            {
                command.StartUp();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!play)                      // ���s����Ă��Ȃ��Ȃ���s�\�����`�F�b�N����
            {
                PlayCheck();
                return;
            }

            if (count > 0)                  // ���Ԍv���p�ϐ���0�ȏ�̐��l�����Ȃ�
            {
                count -= Time.deltaTime;    // ���Ԍv���p�ϐ������炷
                if (count < 0)              // ����ɂ��0�����ɂȂ����ꍇ
                {
                    count = 0.0f;           // �v���p�ϐ�������������
                    OnExecute();            // ���s�C���f�b�N�X�ύX�p�֐�
                }
            }
            else�@                          // ���Ԍv���p�ϐ����l�������Ȃ��ꍇ
            {
                // ���݂̎��s�C���f�b�N�X�̃R�}���h�����s
                playCommand[index].CommandExecute(state, transform);
            }
        }

        /// <summary>
        /// ���s�C���f�b�N�X�ύX�p�֐�
        /// </summary>
        private void OnExecute()
        {
            if (state == CommandState.MOVE_ON)  // �ʏ�ړ��ł���΃C���f�b�N�X�𑝉�
            {
                index++;
            }
            else                                // ���]�ړ��ł���΃C���f�b�N�X�����Z
            {
                index--;
            }

            if (playCommand.Length <= index)    // ���s�C���f�b�N�X���Ǘ��R�}���h�̐��𒴂�����              
            {
                state = CommandState.RETURN;    // �R�}���h�X�e�[�g�𔽓]�ړ��ɂ���
                index--;                        // ���s�C���f�b�N�X�����Z����
                // �Ώۂ̃R�}���h�̗L�����������s��
                playCommand[index].ActionActivate(Complete, gameObject);
            }
            else if (index < 0)                 // ���s�C���f�b�N�X��0�����ł���Ȃ�
            {
                index = 0;                      // ���s�C���f�b�N�X��0�ɐݒ�
                state = CommandState.MOVE_ON;   // �R�}���h�X�e�[�g��ʏ�ړ��ɕύX
                PlayCheck();                    // ���s�\�ł��邩���m�F
                // �Ώۂ̃R�}���h�����݂���ΗL�����������s��
                playCommand[index]?.ActionActivate(Complete, gameObject);   
            }
            else
            {
                // �Ώۂ̃R�}���h�̗L�����������s��
                playCommand[index].ActionActivate(Complete, gameObject);
            }
        }

        /// <summary>
        /// �R�}���h�I�����ɃC���^�[�o����݂��邽�߂̏���
        /// </summary>
        private void Complete()
        {
            count = 1.0f;
        }

        /// <summary>
        /// ���s�\�ł��邩���m�F����֐�
        /// </summary>
        private void PlayCheck()
        {
            if (controlCommand.Length <= 0)         // �R�}���h�Ǘ�����0�������Ȃ�
            {
                play = false;                       // ���s�s�ɕύX
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
                        play = false;
                        playCommand = new MainCommand[3];
                        return;
                    }
                }

                play = true;                                                    // ���s�\�ɕύX
                Array.Copy(controlCommand, playCommand, controlCommand.Length); // �Ǘ��R�}���h�Ɏ��s�R�}���h�̓��e���R�s�[

                // ���s�R�}���h���X�g�̗v�f�S�Ăɏ������֐������s
                foreach (MainCommand command in playCommand)
                {
                    command.StartUp();
                }

                playCommand[0]?.ActionActivate(Complete, gameObject);           // �擪�v�f�ɗL�����֐������s
            }
        }
    }
}
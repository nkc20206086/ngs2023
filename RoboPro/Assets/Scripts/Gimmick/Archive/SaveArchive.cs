using UnityEngine;
using System;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// �Z�[�u���N���X
    /// </summary>
    public class SaveArchive
    {
        public int index { get; private set; }                  // ���̃N���X�����C���f�b�N�X���
        public Transform saveTransform { get; private set; }    // �Ώۂ̍��W�l��

        private MainCommand[] mainCommands;                     // ���C���R�}���h�z��
        private CommandBase[] strageCommands;                   // �X�g���[�W�R�}���h�z��

        /// <summary>
        /// �Z�[�u����郁�C���R�}���h�z��
        /// </summary>
        public MainCommand[] saveMainCommand
        {
            get => mainCommands;                                        // �N���X���ŕۑ�����Ă��郁�C���R�}���h�z���Ԃ�
            set
            {
                MainCommand[] commands = new MainCommand[value.Length]; // ���C���R�}���h�z��̃��[�J���ϐ����쐬����

                // ���C���R�}���h�z��̃��[�J���ϐ��ɁA������ꂽ�z��̃R�s�[���쐬����
                for (int i = 0;i < commands.Length;i++)
                {
                    commands[i] = value[i] != null ? value[i].MainCommandClone() : default;
                }

                mainCommands = commands;                                // �쐬�������[�J���ϐ����N���X���ɕۑ�
            }
        }

        /// <summary>
        /// �Z�[�u����Ă���X�g���[�W�R�}���h�z��
        /// </summary>
        public CommandBase[] saveStrageCommand
        {
            get => strageCommands;                                      // �N���X���ŕۑ�����Ă���X�g���[�W�R�}���h�z���Ԃ�
            set
            {
                CommandBase[] commands = new CommandBase[value.Length]; // �X�g���[�W�R�}���h�z��̃��[�J���ϐ����쐬����

                // �X�g���[�W�R�}���h�z��̃��[�J���ϐ��ɑ�����ꂽ�z��̃R�s�[���쐬����
                for (int i = 0;i < commands.Length;i++)
                {
                    commands[i] = value[i] != null ? value[i].BaseClone() : default;
                }

                strageCommands = commands;                              // �쐬�������[�J���ϐ����N���X���ɕۑ�
            }
        }

        /// <summary>
        /// �R���X�g���N�^(�C���f�b�N�X�A�g�����X�t�H�[���ݒ�p)
        /// </summary>
        /// <param name="index">�ۑ��C���f�b�N�X</param>
        /// <param name="transform">�ۑ��g�����X�t�H�[��</param>
        public SaveArchive(int index, Transform transform)
        {
            // �e�l��ݒ肷��
            this.index = index;
            saveTransform = transform;

            // �R�}���h�z��͌ォ��̐ݒ�ƂȂ邽�߃f�t�H���g�l��ݒ肷��
            mainCommands = default;
            strageCommands = default;
        }
    }
}
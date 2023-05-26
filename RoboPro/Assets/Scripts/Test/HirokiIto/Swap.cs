using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// �R�}���h����ւ��N���X
    /// </summary>
    public class Swap : MonoBehaviour
    {
        [Header("�f�o�b�O�p�\���ϐ��Q(�ύX���Ȃ��ł�������)")]
        [SerializeField, Tooltip("���C���R�}���h�̓���ւ��C���f�b�N�X")]
        int mainIndexNum = -1;
        [SerializeField, Tooltip("�X�g���[�W�R�}���h�̓���ւ��C���f�b�N�X")]
        int storageIndexNum = -1;
        [SerializeField, Tooltip("����ւ��^�C�v")]
        private CommandType swapCommandType;

        [SerializeField, Tooltip("�X�g���[�W�R�}���h���Ǘ�����N���X")]
        private CommandStorage storage;

        [SerializeField, Tooltip("�R�}���h�{�^�����Ǘ�����N���X")]
        private UITest buttonManager;

        private MainCommand[] mainCommands; // ����ւ��Ώۂ̃��C���R�}���h�z����i�[����ϐ�

        private bool isChanged;             // ����ւ��̗L����ۑ����Ă����ϐ�

        private void Start()
        {
            isChanged = true;                                                      // ����ւ��̗L����������
            buttonManager.Intialize(SwitchTypeSet, MainIndexChange, StorageIndexChange);  // �{�^������ւ��N���X������������
        }

        /// <summary>
        /// �e�L�X�g�X�V����
        /// </summary>
        private void TextRewriting()
        {
            buttonManager.MainButtonTextRewriting(mainCommands);                 // �{�^���Ǘ��N���X�Ƀ��C���R�}���h�{�^���̃e�L�X�g�X�V���˗�
            buttonManager.StrageButtonTextRewriting(storage.controlCommand);     // �{�^���Ǘ��N���X�ɃX�g���[�W�R�}���h�{�^���̃e�L�X�g�X�V���˗�
        }

        /// <summary>
        /// �R�}���h����ւ�����
        /// </summary>
        private void CommandSwap()
        {
            if (mainIndexNum < 0 || storageIndexNum < 0) return;                                               // �ǂ��炩�̃C���f�b�N�X��0�����ł���Ȃ瑁�����^�[������
            if (mainCommands[mainIndexNum] == null && storage.controlCommand[storageIndexNum] == null) return; // �Ώۂ̃��C���R�}���h�ƃX�g���[�W�R�}���h�ɒl���Ȃ��Ȃ瑁�����^�[������

            // ����ւ��^�C�v�����C���R�}���h�ł���Ȃ�
            if (swapCommandType == CommandType.Command)
            {
                // ���C���R�}���h�ƃX�g���[�W�R�}���h�̃R�}���h�^�C�v����v���Ă��邩�A�Е���null�ł���ꍇ
                if ((mainCommands[mainIndexNum] == null && storage.controlCommand[storageIndexNum].GetCommandType() == CommandType.Command) ||
                    (storage.controlCommand[storageIndexNum] == null && mainCommands[mainIndexNum] != null) ||
                    mainCommands[mainIndexNum] != null && storage.controlCommand[storageIndexNum].GetCommandType() == CommandType.Command)
                {
                    MainCommand main = mainCommands[mainIndexNum];                                          // ���C���R�}���h�����[�J���ɕۑ�
                    mainCommands[mainIndexNum] = storage.controlCommand[storageIndexNum] as MainCommand;    // �Ώۂ̃��C���R�}���h�ɃX�g���[�W�R�}���h���_�E���L���X�g���đ��
                    storage.controlCommand[storageIndexNum] = main;                                         // �X�g���[�W�R�}���h�Ƀ��[�J���ɕۑ��������C���R�}���h����

                    TextRewriting();                                                                        // �e�L�X�g�X�V

                    // �R�}���h�C���f�b�N�X������������
                    mainIndexNum = -1;
                    storageIndexNum = -1;

                    isChanged = true;                                                                       // �ύX�ς݂ɕύX
                }
            }
            else
            {
                if (mainCommands[mainIndexNum] == null) return;                                               // �Ώۂ̃��C���R�}���h�ɒl���Ȃ��Ȃ瑁�����^�[������

                if (storage.controlCommand[storageIndexNum] == null ||                                        // �X�g���[�W�R�}���h�ɒl���Ȃ���
                    swapCommandType == storage.controlCommand[storageIndexNum].GetCommandType())     // �X�g���[�W�R�}���h�̃R�}���h�^�C�v������ւ��^�C�v�ƈ�v���Ă���Ȃ�
                {
                    // �e�R�}���h�^�C�v�ɉ����ă_�E���L���X�g�����A�l�����ւ���
                    switch (swapCommandType)
                    {
                        case CommandType.Value:
                            ValueCommand num = mainCommands[mainIndexNum].value;
                            mainCommands[mainIndexNum].value = storage.controlCommand[storageIndexNum] as ValueCommand;
                            storage.controlCommand[storageIndexNum] = num;
                            break;
                        case CommandType.Axis:
                            AxisCommand axis = mainCommands[mainIndexNum].axis;
                            mainCommands[mainIndexNum].axis = storage.controlCommand[storageIndexNum] as AxisCommand;
                            storage.controlCommand[storageIndexNum] = axis;
                            break;
                    }

                    TextRewriting();                                                           // �e�L�X�g�X�V

                    // �R�}���h�C���f�b�N�X������������
                    mainIndexNum = -1;
                    storageIndexNum = -1;

                    isChanged = true;                                                          // �ύX�ς݂ɕύX
                }
            }
        }

        /// <summary>
        /// ������int�l���R�}���h�^�C�v�ɕύX���ۑ�����֐�
        /// </summary>
        /// <param name="type">�R�}���h�^�C�v�ɕύX����int�l</param>
        public void SwitchTypeSet(int type)
        {
            swapCommandType = (CommandType)type;
        }

        /// <summary>
        /// �L��������
        /// </summary>
        /// <param name="obj">����ւ��Ώۂ̃��C���R�}���h�z��</param>
        public void SwitchActivation(MainCommand[] obj)
        {
            mainCommands = obj;                // ���C���R�}���h�z����N���X���ɕۑ�

            buttonManager.CanvasDisplay();     // �{�^���\���L�����o�X��\������

            TextRewriting();                   // �e�L�X�g�X�V����

        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <returns>�ύX���s��ꂽ��</returns>
        public bool SwitchInvalidation()
        {
            buttonManager.CanvasHide();                     // �{�^���\���L�����o�X���\���ɂ���

            if (isChanged)                                  // �ύX���s���Ă���Ȃ�
            {
                isChanged = false;                          // �ύX�󋵂�������

                return true;                                // �ύX�ς݂ł���Ƒ��M
            }

            return false;                                   // �ύX����Ă��Ȃ��Ƒ��M
        }

        /// <summary>
        /// ���C���R�}���h�C���f�b�N�X��o�^����֐�
        /// </summary>
        /// <param name="index">�ΏۃC���f�b�N�X</param>
        private void MainIndexChange(int index)
        {
            mainIndexNum = index; // ���C���R�}���h�C���f�b�N�X��ۑ�

            CommandSwap();        // �R�}���h����ւ��֐������s
        }

        /// <summary>
        /// �X�g���[�W�R�}���h�C���f�b�N�X��o�^����֐�
        /// </summary>
        /// <param name="index">�ΏۃC���f�b�N�X</param>
        private void StorageIndexChange(int index)
        {
            storageIndexNum = index;  // �X�g���[�W�R�}���h�C���f�b�N�X��ۑ�

            CommandSwap();            // �R�}���h����ւ��֐������s
        }
    }
}

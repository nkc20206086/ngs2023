using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command.Entity;

namespace Command
{
    /// <summary>
    /// �R�}���h����ւ��N���X
    /// </summary>
    public class CommandSwitchManager : MonoBehaviour
    {
        [Header("�f�o�b�O�p�\���ϐ��Q(�ύX���Ȃ��ł�������)")]
        [SerializeField,Tooltip("���C���R�}���h�̓���ւ��C���f�b�N�X")]
        int index_main = -1;
        [SerializeField,Tooltip("�X�g���[�W�R�}���h�̓���ւ��C���f�b�N�X")]
        int index_strage = -1;
        [SerializeField,Tooltip("����ւ��^�C�v")]
        private CommandType switchType;

        [SerializeField,Tooltip("�X�g���[�W�R�}���h���Ǘ�����N���X")]
        private CommandStrage strage;

        [SerializeField,Tooltip("�R�}���h�{�^�����Ǘ�����N���X")]
        private CommandButtonManager buttonManager;

        private MainCommand[] mainCommands; // ����ւ��Ώۂ̃��C���R�}���h�z����i�[����ϐ�
        private SaveArchive saveItem;       // �Z�[�u���N���X���i�[����ϐ�

        private bool change = true;         // ����ւ��̗L����ۑ����Ă����ϐ�

        private void Start()
        {
            buttonManager.Intialize(SwitchTypeSet, ChoosingMain, ChoosingStrage);  // �{�^������ւ��N���X������������
        }

        /// <summary>
        /// �e�L�X�g�X�V����
        /// </summary>
        private void TextUpdate()
        {
            buttonManager.MainButtonTextUpdate(mainCommands);       // �{�^���Ǘ��N���X�Ƀ��C���R�}���h�{�^���̃e�L�X�g�X�V���˗�
            buttonManager.StrageButtonTextUpdate(strage.bases);     // �{�^���Ǘ��N���X�ɃX�g���[�W�R�}���h�{�^���̃e�L�X�g�X�V���˗�
        }

        /// <summary>
        /// �R�}���h����ւ�����
        /// </summary>
        private void CommandSwitch()
        {
            if ((index_main >= 0 && index_strage >= 0) &&                                      // ���C���R�}���h�C���f�b�N�X�ƃX�g���[�W�R�}���h�C���f�b�N�X�̗����ɒl�����݂��A
                (mainCommands[index_main] == strage.bases[index_strage] ||                     // ���C���R�}���h�ƃX�g���[�W�R�}���h����v���Ă��邩
                (strage.bases[index_strage] == null && mainCommands[index_main] != null) ||    // ���C���R�}���h�ƃX�g���[�W�R�}���h�̂ǂ��炩�݂̂ɒl�����݂��邩
                strage.bases[index_strage] != null && mainCommands[index_main] == null ||
                !mainCommands[index_main].CommandNullCheck()))                              // �l�̂ǂ��炩��null�ł���Ȃ�
            {
                // �R�}���h�^�C�v���擾
                CommandType itemType = strage.bases[index_strage] == null ? switchType : strage.bases[index_strage].ConfirmationCommandType();

                // �e�R�}���h�^�C�v�ɉ����ă_�E���L���X�g�����A�l�����ւ���
                switch (itemType)
                {
                    case CommandType.Num:
                        NumCommand num = mainCommands[index_main].num;
                        mainCommands[index_main].num = strage.bases[index_strage] as NumCommand;
                        strage.bases[index_strage] = num;
                        break;
                    case CommandType.Axis:
                        AxisCommand axis = mainCommands[index_main].axis;
                        mainCommands[index_main].axis = strage.bases[index_strage] as AxisCommand;
                        strage.bases[index_strage] = axis;
                        break;
                    case CommandType.Command:
                        MainCommand main = mainCommands[index_main];
                        mainCommands[index_main] = strage.bases[index_strage] as MainCommand;
                        strage.bases[index_strage] = main;
                        break;
                }

                TextUpdate();       // �e�L�X�g�X�V

                // �R�}���h�C���f�b�N�X������������
                index_main = -1;    
                index_strage = -1;

                change = true;      // �ύX�ς݂ɕύX
            }
        }

        /// <summary>
        /// ������int�l���R�}���h�^�C�v�ɕύX���ۑ�����֐�
        /// </summary>
        /// <param name="type">�R�}���h�^�C�v�ɕύX����int�l</param>
        public void SwitchTypeSet(int type)
        {
            switchType = (CommandType)type;
        }

        /// <summary>
        /// �L��������
        /// </summary>
        /// <param name="obj">����ւ��Ώۂ̃��C���R�}���h�z��</param>
        /// <param name="item">�Z�[�u���N���X</param>
        public void SwitchActivate(MainCommand[] obj, SaveArchive item)
        {
            mainCommands = obj;             // ���C���R�}���h�z����N���X���ɕۑ�

            saveItem = item;                //  �Z�[�u���N���X���N���X���ɕۑ�

            buttonManager.CanvasDisplay();  // �{�^���\���L�����o�X��\������

            TextUpdate();                   // �e�L�X�g�X�V����

        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <returns>�ύX���s��ꂽ��</returns>
        public bool TextInactive()
        {
            buttonManager.CanvasHide();                     // �{�^���\���L�����o�X���\���ɂ���

            if (change)                                     // �ύX���s���Ă���Ȃ�
            {
                // �Z�[�u���N���X�Ɋe�R�}���h�󋵂�ۑ�
                saveItem.saveMainCommand = mainCommands;    
                saveItem.saveStrageCommand = strage.bases;

                change = false;                             // �ύX�󋵂�������

                return true;                                // �ύX�ς݂ł���Ƒ��M
            }

            return false;                                   // �ύX����Ă��Ȃ��Ƒ��M
        }

        /// <summary>
        /// ���C���R�}���h�C���f�b�N�X��o�^����֐�
        /// </summary>
        /// <param name="index">�ΏۃC���f�b�N�X</param>
        private void ChoosingMain(int index)
        {
            index_main = index; // ���C���R�}���h�C���f�b�N�X��ۑ�

            CommandSwitch();    // �R�}���h����ւ��֐������s
        }

        /// <summary>
        /// �X�g���[�W�R�}���h�C���f�b�N�X��o�^����֐�
        /// </summary>
        /// <param name="index">�ΏۃC���f�b�N�X</param>
        private void ChoosingStrage(int index)
        {
            index_strage = index;  // �X�g���[�W�R�}���h�C���f�b�N�X��ۑ�

            CommandSwitch();    // �R�}���h����ւ��֐������s
        }
    }
}
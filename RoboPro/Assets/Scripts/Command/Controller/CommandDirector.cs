using UnityEngine;
using System;
using Command.Entity;

namespace Command
{
    // �R�}���h�֘A�̏������Ǘ�����N���X
    public class CommandDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("�R�}���h����ւ��N���X")]
        private CommandSwapManager commandSwapManager;

        [SerializeField, Tooltip("")]
        private CommandStorage commandStorage;

        public event Action<MainCommand[]> UIEvent_MainCommands;
        public event Action<CommandBase[]> UIEvent_StorageCommands;
        public event Action<bool> UIOpen; 

        /// <summary>
        /// �R�}���h����ւ���L��������
        /// </summary>
        /// <param name="mainCommands">����ւ��Ώۂ̃R�}���h�z��</param>
        /// <param name="item">�Z�[�u���N���X</param>
        public void CommandActivation(MainCommand[] mainCommands)
        {
            // ����ւ��N���X��L��������
            commandSwapManager.SwapActivation(mainCommands);

            UIOpen?.Invoke(true);
            UIEvent_MainCommands?.Invoke(mainCommands);
            UIEvent_StorageCommands?.Invoke(commandStorage.controlCommand);
        }

        /// <summary>
        /// �R�}���h����ւ��𖳌�������
        /// </summary>
        /// <returns>�ύX���s��ꂽ��</returns>
        public bool CommandInvalidation()
        {
            UIOpen?.Invoke(false);
            // ����ւ��N���X�𖳌������A�ύX�̗L�����󂯎�著�M
            bool retValue = commandSwapManager.SwapInvalidation();
            return retValue;
        }

        private void UIChange(MainCommand[] mainCommands)
        {
            UIEvent_MainCommands(mainCommands);
            UIEvent_StorageCommands(commandStorage.controlCommand);
        }
    }

}
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

        public event Action showUI;
        public event Action hideUI;

        public event Action<MainCommand[]> swapUI_MainCommand;
        public event Action<CommandBase[]> swapUI_Storage;

        /// <summary>
        /// �R�}���h����ւ���L��������
        /// </summary>
        /// <param name="mainCommands">����ւ��Ώۂ̃R�}���h�z��</param>
        /// <param name="item">�Z�[�u���N���X</param>
        public void CommandActivation(MainCommand[] mainCommands)
        {
            commandSwapManager.action += SetCommandText;
            // ����ւ��N���X��L��������
            commandSwapManager.SwapActivation(mainCommands);
            SetCommandText(mainCommands);

            showUI?.Invoke();
        }

        /// <summary>
        /// �R�}���h����ւ��𖳌�������
        /// </summary>
        /// <returns>�ύX���s��ꂽ��</returns>
        public bool CommandInvalidation()
        {
            // ����ւ��N���X�𖳌������A�ύX�̗L�����󂯎�著�M
            bool retValue = commandSwapManager.SwapInvalidation();
            hideUI?.Invoke();
            return retValue;
        }

        public Action<int,int> GetMainCommandIndexSet()
        {
            return commandSwapManager.SetMainCommandIndex;
        }

        public Action<int,int> GetStorageIndexSet()
        {
            return commandSwapManager.SetStorageIndex;
        }

        private void SetCommandText(MainCommand[] mainCommands)
        {
            swapUI_MainCommand?.Invoke(mainCommands);
            swapUI_Storage?.Invoke(commandStorage.controlCommand);
        }
    }
}
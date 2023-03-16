using UnityEngine;
using Command.Entity;

namespace Command
{
    // �R�}���h�֘A�̏������Ǘ�����N���X
    public class CommandDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("�R�}���h����ւ��N���X")]
        private CommandSwapManager commandSwitchManager;

        /// <summary>
        /// �R�}���h����ւ���L��������
        /// </summary>
        /// <param name="mainCommands">����ւ��Ώۂ̃R�}���h�z��</param>
        /// <param name="item">�Z�[�u���N���X</param>
        public void CommandActivation(MainCommand[] mainCommands)
        {
            // ����ւ��N���X��L��������
            commandSwitchManager.SwitchActivation(mainCommands);
        }

        /// <summary>
        /// �R�}���h����ւ��𖳌�������
        /// </summary>
        /// <returns>�ύX���s��ꂽ��</returns>
        public bool CommandInvalidation()
        {
            // ����ւ��N���X�𖳌������A�ύX�̗L�����󂯎�著�M
            bool retValue = commandSwitchManager.SwitchInvalidation();
            return retValue;
        }
    }

}
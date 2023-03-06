using UnityEngine;
using Command.Entity;

namespace Command
{
    // �R�}���h�֘A�̏������Ǘ�����N���X
    public class CommandDirector : MonoBehaviour
    {
        [SerializeField,Tooltip("�R�}���h����ւ��N���X")]
        private CommandSwitchManager commandSwitchManager;

        /// <summary>
        /// �R�}���h����ւ���L��������
        /// </summary>
        /// <param name="mainCommands">����ւ��Ώۂ̃R�}���h�z��</param>
        /// <param name="item">�Z�[�u���N���X</param>
        public void CommandActivate(MainCommand[] mainCommands, SaveArchive item)
        {
            // ����ւ��N���X��L��������
            commandSwitchManager.SwitchActivate(mainCommands,item);
        }

        /// <summary>
        /// �R�}���h����ւ��𖳌�������
        /// </summary>
        /// <returns>�ύX���s��ꂽ��</returns>
        public bool CommandInactive()
        {
            // ����ւ��N���X�𖳌������A�ύX�̗L�����󂯎�著�M
            bool retValue = commandSwitchManager.TextInactive();
            return retValue;
        }
    }

}
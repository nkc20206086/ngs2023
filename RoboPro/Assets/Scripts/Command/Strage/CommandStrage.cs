using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Command.Entity
{
    /// <summary>
    /// �X�g���[�W�R�}���h���Ǘ�����N���X
    /// </summary>
    public class CommandStrage : MonoBehaviour
    {
        [SerializeField,Tooltip("�X�g���[�W�R�}���h�̐�")]
        private int strageCount = 0;

        public CommandBase[] bases; // �Ǘ����Ă���X�g���[�W�R�}���h

        private void Awake()
        {
            bases = new CommandBase[strageCount];   // �ݒ肳�ꂽ���ŃX�g���[�W�R�}���h�z���������
        }
    }
}
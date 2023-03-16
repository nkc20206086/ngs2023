using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// ���l�R�}���h�N���X
    /// </summary>
    public class ValueCommand : CommandBase
    {
        private int value;    // ���̃N���X�̎����l

        /// <summary>
        /// �R���X�g���N�^(�ϐ��̓R���X�g���N�^�ł̂ݐݒ�\�ł�)
        /// </summary>
        /// <param name="value">�ݒ肷�鐔�l</param>
        public ValueCommand(int value)
        {
            this.value = value;
        }

        public override string GetString()
        {
            return value.ToString();
        }

        public override CommandType ConfirmCommandType()
        {
            return CommandType.Value;
        }

        /// <summary>
        /// ���̃N���X�̎����l��Ԃ�
        /// </summary>
        public int valueGet
        {
            get => value;
        }
    }
}
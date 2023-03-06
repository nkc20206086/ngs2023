using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// ���l�R�}���h�N���X
    /// </summary>
    public class NumCommand : CommandBase
    {
        private int num;    // ���̃N���X�̎����l

        /// <summary>
        /// �R���X�g���N�^(�ϐ��̓R���X�g���N�^�ł̂ݐݒ�\�ł�)
        /// </summary>
        /// <param name="num">�ݒ肷�鐔�l</param>
        public NumCommand(int num)
        {
            this.num = num;
        }

        public override string GetString()
        {
            return num.ToString();
        }

        public override void StartUp() { }

        public override CommandType ConfirmationCommandType()
        {
            return CommandType.Num;
        }

        /// <summary>
        /// ���̃N���X�̎����l��Ԃ�
        /// </summary>
        public int numGet
        {
            get => num;
        }
    }
}
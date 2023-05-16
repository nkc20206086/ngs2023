using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// ���R�}���h�N���X
    /// </summary>
    public class AxisCommand : CommandBase
    {
        private CoordinateAxis axis;    // ���̃N���X�̎���

        /// <summary>
        /// �R���X�g���N�^(�ϐ��̓R���X�g���N�^�ł̂ݐݒ�\�ł�)
        /// </summary>
        /// <param name="axis">�ݒ肷�鎲</param>
        public AxisCommand(CoordinateAxis axis)
        {
            this.axis = axis;
        }

        public override string GetString()
        {
            return axis.ToString(); // enum�𕶎��񉻂������̂�Ԃ�
        }

        public override CommandType ConfirmCommandType()
        {
            return CommandType.Axis;
        }

        /// <summary>
        /// ���̃N���X�̎�����Ԃ�
        /// </summary>
        public CoordinateAxis getAxis
        {
            get => axis;
        }
    }
}
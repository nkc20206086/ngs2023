using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// ��]���s���R�}���h�N���X
    /// </summary>
    public class RotateCommand : MainCommand
    {
        const int ANGLE_MAG = 90;
        const float ROTATE_MAG = 2.0f;

        private float angle;
        private Quaternion baseQuat;    // ���s�O�̉�]��

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public RotateCommand(CommandContainer status) : base(status) { }

        public override object InitCommand(object target,Action completeAction)
        {
            // �e���ڂ����݂̏�ԂōĐݒ�
            this.completeAction = completeAction;
            usableValue = value.getValue * ANGLE_MAG;
            usableAxis = axis.getAxis;

            Quaternion quaternion = (Quaternion)target;                                         // ������]�l����������擾����
            baseQuat = quaternion;                                                              // �ϐ��ɕۑ�

            return quaternion * Quaternion.Euler(GetDirection() * Mathf.Abs(value.getValue * ANGLE_MAG));   // �l�𔽉f������]�l��Ԃ�
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                                                 // �󋵂��������ł���Ύ��s���Ȃ�

            if (state == CommandState.MOVE_ON)
            {
                if (angle >= Mathf.Abs(usableValue))                                                                    // �w�肳�ꂽ�p�x����]���Ă����
                {
                    targetTransform.rotation = baseQuat * Quaternion.Euler(GetDirection() * Mathf.Abs(usableValue));    // eulerAngle��������]�l�ɉ�]�𔽉f�����l�ɂ���
                    completeAction?.Invoke();                                                                           // �R�}���h�������A�N�V���������s����
                }
                else
                {
                    angle += ROTATE_MAG;                                                                                // �p�x���Z
                    targetTransform.rotation *= Quaternion.Euler(GetDirection() * ROTATE_MAG);                          // ��]����
                }
            }
            else if (state == CommandState.RETURN)
            {
                if (angle <= 1)                                                                                         // �p�x�����̒l�ɋ߂Â����Ȃ�
                {
                    targetTransform.rotation = baseQuat;                                                                // ������]�l�ɖ߂�
                    completeAction?.Invoke();                                                                           // �R�}���h�������A�N�V���������s����
                }
                else
                {
                    angle -= ROTATE_MAG;                                                                                // �p�x���Z
                    targetTransform.rotation *= Quaternion.Euler(GetDirection() * -ROTATE_MAG);                         // �t��]����
                }
            }
        }

        public override MainCommandType GetMainCommandType()
        {
            return MainCommandType.Rotate;
        }

        public override string GetString()
        {
            return "��]";
        }
    }
}
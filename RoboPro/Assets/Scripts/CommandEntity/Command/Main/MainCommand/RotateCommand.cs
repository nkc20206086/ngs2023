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
        private Quaternion baseQuat;    // ���s�O�̉�]��

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public RotateCommand(CommandStruct status) : base(status) { }

        public override object InitCommand(object target,Action completeAction)
        {
            // �e���ڂ����݂̏�ԂōĐݒ�
            this.completeAction = completeAction;
            usableValue = value.getValue;
            usableAxis = axis.getAxis;

            Quaternion quaternion = (Quaternion)target;                                         // ������]�l����������擾����
            baseQuat = quaternion;                                                              // �ϐ��ɕۑ�

            return quaternion * Quaternion.Euler(GetDirection() * Mathf.Abs(value.getValue));   // �l�𔽉f������]�l��Ԃ�
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                                                 // �󋵂��������ł���Ύ��s���Ȃ�

            if (state == CommandState.MOVE_ON)
            {
                if (Mathf.Abs(Quaternion.Angle(baseQuat, targetTransform.rotation)) > Mathf.Abs(usableValue))           // �w�肳�ꂽ�p�x����]���Ă����
                {
                    targetTransform.rotation = baseQuat * Quaternion.Euler(GetDirection() * Mathf.Abs(usableValue));    // eulerAngle��������]�l�ɉ�]�𔽉f�����l�ɂ���
                    completeAction?.Invoke();                                                                           // �R�}���h�������A�N�V���������s����
                }
                else
                {
                    targetTransform.Rotate(GetDirection());                                                             // ��]����
                }
            }
            else if (state == CommandState.RETURN)
            {
                if (Mathf.Abs(Quaternion.Angle(baseQuat, targetTransform.rotation)) < 1)                                // ���s�O��]�ʂɋ߂Â�����
                {
                    targetTransform.rotation = baseQuat;                                                                // ������]�l�ɖ߂�
                    completeAction?.Invoke();                                                                           // �R�}���h�������A�N�V���������s����
                }
                else
                {
                    targetTransform.Rotate(GetDirection() * -1);                                                        // �t��]����
                }
            }
        }

        public override MainCommandType GetMainCommandType()
        {
            return MainCommandType.Rotate;
        }
    }
}
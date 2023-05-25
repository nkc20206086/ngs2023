using System;
using UnityEngine;

namespace Command.Entity
{
    /// <summary>
    /// �ړ����s���R�}���h�N���X
    /// </summary>
    public class MoveCommand : MainCommand
    {
        private Vector3 basePos;    // �ړ��O�̍��W

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public MoveCommand(CommandContainer status) : base(status) { }

        public override object InitCommand(object target, Action completeAction)
        {
            // �e���ڂ����݂̏�ԂōĐݒ�
            this.completeAction = completeAction;
            usableValue = value.getValue;
            usableAxis = axis.getAxis;

            basePos = (Vector3)target;                          // ����������W���擾���A�ړ��O���W�ɕۑ�����
            return GetDirection() * Mathf.Abs(value.getValue);  // �ړ��ʂ�Ԃ�
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                             // �������X�e�[�g�𑗐M����Ă���Ȃ瑁�����^�[������

            if (state == CommandState.MOVE_ON)
            {
                if (Vector3.Distance(basePos, targetTransform.position) > Mathf.Abs(usableValue))   // ���_����̈ړ��������ݒ萔�l�𒴂��Ă���Ȃ�
                {
                    targetTransform.position = basePos + (GetDirection() * Mathf.Abs(usableValue)); // �Ώۂ̈ʒu��Ώۂ̍��W�ɕύX
                    completeAction?.Invoke();                                                       // �R�}���h���������������s
                }
                else                                                                                // �܂��ړ��������ݒ萔�l�𒴂��Ă��Ȃ��Ȃ�
                {
                    targetTransform.position += GetDirection();                                     // ���W�l��{���𔽉f�������l���ړ�����
                }
            }
            else
            {
                if (Vector3.Distance(basePos, targetTransform.position) < 1)                        // ���_����̈ړ��������ݒ萔�l�𒴂��Ă���Ȃ�
                {
                    targetTransform.position = basePos;                                             // �Ώۂ̈ʒu��Ώۂ̍��W�ɕύX
                    completeAction?.Invoke();                                                       // �R�}���h���������������s
                }
                else                                                                                // �܂��ړ��������ݒ萔�l�𒴂��Ă��Ȃ��Ȃ�
                {
                    targetTransform.position += GetDirection() * -1;                                // ���W�l��{���𔽉f�������l���ړ�����
                }
            }
        }

        public override MainCommandType GetMainCommandType()
        {
            return MainCommandType.Move;
        }
    }
}


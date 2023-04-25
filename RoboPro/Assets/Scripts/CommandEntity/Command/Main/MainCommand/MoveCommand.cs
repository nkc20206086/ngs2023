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
        public MoveCommand(CommandStruct status) : base(status) { }

        public override object StartUp(object target, Action completeAction)
        {
            this.completeAction = completeAction;
            usableValue = value.getValue;
            usableAxis = axis.getAxis;

            basePos = (Vector3)target;

            return GetDirection() * value.getValue;
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                             // �������X�e�[�g�𑗐M����Ă���Ȃ瑁�����^�[������

            if (state == CommandState.MOVE_ON)
            {
                if (Vector3.Distance(basePos, targetTransform.position) > Mathf.Abs(usableValue))   // ���_����̈ړ��������ݒ萔�l�𒴂��Ă���Ȃ�
                {
                    targetTransform.position = basePos + (GetDirection() * usableValue);            // �Ώۂ̈ʒu��Ώۂ̍��W�ɕύX
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


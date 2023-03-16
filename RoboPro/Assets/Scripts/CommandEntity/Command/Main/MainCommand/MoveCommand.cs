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
        /// �R���X�g���N�^ ���l���ڐݒ�p
        /// </summary>
        /// <param name="lock_m">�R�}���h��ύX�\���ǂ���</param>
        /// <param name="lock_n">���l��ύX�\���ǂ���</param>
        /// <param name="lock_c">����ύX�\���ǂ���</param>
        /// <param name="methodName">�R�}���h�̖���</param>
        /// <param name="num">���l�ɗp����l</param>
        /// <param name="axis">���ɗp����l</param>
        /// <param name="capacity">���̃R�}���h���v����e��</param>
        public MoveCommand(bool lock_m, bool lock_n, bool lock_c,string methodName, int num, int axis, int capacity) 
             : base(lock_m, lock_n, lock_c, methodName, num, axis, capacity) { }

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public MoveCommand(CommandStruct status) : base(status) { }

        public override void ActionActivate(Action completeAction, GameObject obj)
        {
            base.ActionActivate(completeAction,obj);
            basePos = obj.transform.position;
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                         // �������X�e�[�g�𑗐M����Ă���Ȃ瑁�����^�[������

            int mag = state == CommandState.MOVE_ON ? 1 : -1;                                   // ���������ɂ���Ĕ{����ݒ肷��

            if (Vector3.Distance(basePos, targetTransform.position) > Mathf.Abs(usableValue))     // ���_����̈ړ��������ݒ萔�l�𒴂��Ă���Ȃ�
            {
                targetTransform.position = basePos + (GetDirection() * Mathf.Abs(usableValue)) * mag;   // �Ώۂ̈ʒu��Ώۂ̍��W�ɕύX
                completeAction?.Invoke();                                                       // �R�}���h���������������s
            }
            else                                                                                // �܂��ړ��������ݒ萔�l�𒴂��Ă��Ȃ��Ȃ�
            {
                targetTransform.position += GetDirection() * mag;                                     // ���W�l��{���𔽉f�������l���ړ�����
            }
        }
    }
}


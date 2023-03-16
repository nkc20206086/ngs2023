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
        private Vector3 baseAngle;      // ���s�O�̉�]�l
        private Quaternion baseQuat;    // ���s�O�̉�]��

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
        public RotateCommand(bool lock_m, bool lock_n, bool lock_c,
                          string methodName, int num, int axis, int capacity) : base(lock_m, lock_n, lock_c, methodName, num, axis, capacity) { }

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public RotateCommand(CommandStruct status) : base(status) { }

        public override void ActionActivate(Action completeAction, GameObject obj)
        {
            base.ActionActivate(completeAction,obj);
            baseAngle = obj.transform.eulerAngles;
            baseQuat = obj.transform.rotation;
        }

        public override void CommandExecute(CommandState state, Transform targetTransform)
        {
            if (state == CommandState.INACTIVE) return;                                                     // �󋵂��������ł���Ύ��s���Ȃ�

            int mag = state == CommandState.MOVE_ON ? 1 : -1;                                               // �������{��(�t�Đ��ł����-1�����������������𔽓]���邽��)

            if (Mathf.Abs(Quaternion.Angle(baseQuat, targetTransform.rotation)) > Mathf.Abs(usableValue))     // �w�肳�ꂽ�p�x����]���Ă����
            {
                targetTransform.eulerAngles = baseAngle + (GetDirection() * Mathf.Abs(usableValue) * mag) ;         // eulerAngle��������]�l�ɉ�]�𔽉f�����l�ɂ���
                completeAction?.Invoke();                                                                   // �R�}���h�������A�N�V���������s����
            }
            else
            {
                targetTransform.Rotate(GetDirection() * mag);                                                     // �����ɔ{���𔽉f�����l��]����
            }
        }
    }
}
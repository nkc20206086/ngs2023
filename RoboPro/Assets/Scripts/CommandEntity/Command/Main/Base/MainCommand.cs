using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Command.Entity
{
    /// <summary>
    /// �R�}���h�̊��N���X
    /// </summary>
    public class MainCommand : CommandBase
    {
        // �e��l���ύX�\�ł��邩
        public bool lockMenber { get; protected set; }
        public bool lockValue { get; protected set; }
        public bool lockCoordinateAxis { get; protected set; }
        public bool sigined { get; protected set; }

        public ValueCommand value;      // ���l���������R�}���h�N���X
        public AxisCommand axis;        // �����������R�}���h�N���X

        protected int usableValue;              // �R�}���h���p���鐔�l
        protected CoordinateAxis usableAxis;    // �R�}���h���p���鎲

        protected Action completeAction;        // �R�}���h�������Ɏ��s����A�N�V������ۑ�����ϐ�

        public MainCommand() 
        {
            lockMenber = false;
            lockCoordinateAxis = false;
            lockValue = false;
        }

        /// <summary>
        /// �R���X�g���N�^ ���l���ڐݒ�p
        /// </summary>
        /// <param name="lockMenber">�R�}���h��ύX�\���ǂ���</param>
        /// <param name="lockValue">���l��ύX�\���ǂ���</param>
        /// <param name="lockCoordinateAxis">����ύX�\���ǂ���</param>
        /// <param name="commandName">�R�}���h�̖���</param>
        /// <param name="value">���l�ɗp����l</param>
        /// <param name="axis">���ɗp����l</param>
        public MainCommand(bool lockMenber,bool lockValue,bool lockCoordinateAxis,string commandName,int value,int axis) 
        {
            this.lockMenber = lockMenber;
            this.lockValue = lockValue;
            this.lockCoordinateAxis = lockCoordinateAxis;
            this.value = new ValueCommand(value);
            this.axis = new AxisCommand((CoordinateAxis)axis);
            sigined = value > 0 ? true : false;
        }

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public MainCommand(CommandContainer status)
        {
            lockMenber = status.lockCommand;
            lockValue = status.lockNumber;
            lockCoordinateAxis = status.lockCoordinateAxis;
            value = new ValueCommand(status.value);
            axis = new AxisCommand(status.axis);
            sigined = value.getValue > 0 ? true : false;
        }

        /// <summary>
        /// ���C���R�}���h�^�Ƃ��ẴR�s�[��Ԃ��֐�
        /// </summary>
        /// <returns></returns>
        public MainCommand MainCommandClone()
        {
            value = value?.BaseClone() as ValueCommand;
            return (MainCommand)MemberwiseClone();
        }

        /// <summary>
        /// �R�}���h���s�֐�
        /// </summary>
        /// <param name="state">�ǂ��������󋵂œ�������</param>
        /// <param name="targetTransform">�Ώۂ̊e��l�ύX�ptransform</param>
        public virtual void CommandExecute(CommandState state, Transform targetTransform) { }

        /// <summary>
        /// �J�n������
        /// </summary>
        public virtual object InitCommand(object target,Action completeAction)
        {
            usableValue = value.getValue;
            usableAxis = axis.getAxis;
            this.completeAction = completeAction;
            return default;
        }

        /// <summary>
        /// ���C���R�}���h�^�C�v�擾�p�֐�
        /// </summary>
        /// <returns></returns>
        public virtual MainCommandType GetMainCommandType()
        {
            return MainCommandType.None;
        }

        /// <summary>
        /// �������擾����֐�
        /// </summary>
        /// <returns>���̃R�}���h�̎�����</returns>
        protected Vector3 GetDirection()
        {
            Vector3 returnVec = Vector3.zero;
            switch (usableAxis)
            {
                case CoordinateAxis.X: returnVec = Vector3.right; break;
                case CoordinateAxis.Y: returnVec = Vector3.up; break;
                case CoordinateAxis.Z: returnVec = Vector3.forward; break;
            }

            if (usableValue < 0) returnVec *= -1;

            return returnVec;
        }

        public void SignChange()
        {
            sigined = sigined ? false : true;
        }

        /// <summary>
        /// ���l�̃R�}���h�e�L�X�g���擾����֐�
        /// </summary>
        /// <returns></returns>
        public string GetValueText()
        {
            return value != null ? value.GetString() : default;
        }

        /// <summary>
        /// ���̃R�}���h�e�L�X�g���擾����֐�
        /// </summary>
        /// <returns></returns>
        public string GetAxisText()
        {
            return axis != null ? axis.GetString() : default;
        }

        /// <summary>
        /// �R�}���h�̎��l��Ԃ�
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return value != null ? value.getValue : 0;
        }

        /// <summary>
        /// �R�}���h�̎�����Ԃ�
        /// </summary>
        /// <returns></returns>
        public CoordinateAxis GetAxis()
        {
            return axis != null ? axis.getAxis : CoordinateAxis.NONE;
        }

        /// <summary>
        /// �R�}���h�N���X��null�łȂ������m�F����֐�
        /// </summary>
        /// <returns></returns>
        public bool CommandNullCheck()
        {
            return value != null && axis != null;
        }

        public override string GetString()
        {
            return GetCommandType().ToString();
        }

        public override CommandType GetCommandType()
        {
            return CommandType.Command;
        }
    }
}
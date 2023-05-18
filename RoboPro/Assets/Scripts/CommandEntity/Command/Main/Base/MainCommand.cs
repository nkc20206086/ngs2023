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
        protected bool lockMenber = false;
        protected bool lockValue = false;
        protected bool lockCoordinateAxis = false;

        protected string commandName;            // �R�}���h�̖���
        public ValueCommand value;       // ���l���������R�}���h�N���X
        public AxisCommand axis;     // �����������R�}���h�N���X

        protected int usableValue;                // �R�}���h���p���鐔�l
        protected CoordinateAxis usableAxis;    // �R�}���h���p���鎲

        protected int capacity;                 // �R�}���h�̗e��

        protected Action completeAction;        // �R�}���h�������Ɏ��s����A�N�V������ۑ�����ϐ�

        /// <summary>
        /// �R���X�g���N�^ ���l���ڐݒ�p
        /// </summary>
        /// <param name="lockMenber">�R�}���h��ύX�\���ǂ���</param>
        /// <param name="lockValue">���l��ύX�\���ǂ���</param>
        /// <param name="lockCoordinateAxis">����ύX�\���ǂ���</param>
        /// <param name="commandName">�R�}���h�̖���</param>
        /// <param name="value">���l�ɗp����l</param>
        /// <param name="axis">���ɗp����l</param>
        /// <param name="capacity">���̃R�}���h���v����e��</param>
        public MainCommand(bool lockMenber,bool lockValue,bool lockCoordinateAxis,string commandName,int value,int axis,int capacity) 
        {
            this.lockMenber = lockMenber;
            this.lockValue = lockValue;
            this.lockCoordinateAxis = lockCoordinateAxis;
            this.commandName = commandName;
            this.value = new ValueCommand(value);
            this.axis = new AxisCommand((CoordinateAxis)axis);
            this.capacity = capacity;
        }

        /// <summary>
        /// �R���X�g���N�^�@�\���̐ݒ�p
        /// </summary>
        /// <param name="status">�ݒ�p�\����</param>
        public MainCommand(CommandStruct status)
        {
            lockMenber = status.lockCommand;
            lockValue = status.lockNumber;
            lockCoordinateAxis = status.lockCoordinateAxis;
            commandName = status.commandType.ToString();
            value = new ValueCommand(status.value);
            axis = new AxisCommand(status.axis);
            capacity = status.capacity;
        }

        /// <summary>
        /// ���C���R�}���h�^�Ƃ��ẴR�s�[��Ԃ��֐�
        /// </summary>
        /// <returns></returns>
        public MainCommand MainCommandClone()
        {
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

        /// <summary>
        /// �R�}���h�̖��̂��擾����֐�
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return commandName;
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
        /// �R�}���h�N���X��null�łȂ������m�F����֐�
        /// </summary>
        /// <returns></returns>
        public bool CommandNullCheck()
        {
            return value != null && axis != null;
        }

        public override string GetString()
        {
            return $"{commandName} {(axis != null ? axis.GetString() : default)} {(value != null ? value.GetString() : default)}";
        }

        public override CommandType GetCommandType()
        {
            return CommandType.Command;
        }

    }
}
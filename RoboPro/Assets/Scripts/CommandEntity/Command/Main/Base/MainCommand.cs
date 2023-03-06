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
        protected bool lockNumber = false;
        protected bool lockCoordinateAxis = false;

        protected string commandName;            // �R�}���h�̖���
        public NumCommand num;       // ���l���������R�}���h�N���X
        public AxisCommand axis;     // �����������R�}���h�N���X

        protected int num_local;                // �R�}���h���p���鐔�l
        protected CoordinateAxis axis_local;    // �R�}���h���p���鎲

        protected int capacity;                 // �R�}���h�̗e��

        protected Action completeAction;        // �R�}���h�������Ɏ��s����A�N�V������ۑ�����ϐ�

        /// <summary>
        /// �R���X�g���N�^ ���l���ڐݒ�p
        /// </summary>
        /// <param name="lock_m">�R�}���h��ύX�\���ǂ���</param>
        /// <param name="lock_n">���l��ύX�\���ǂ���</param>
        /// <param name="lock_c">����ύX�\���ǂ���</param>
        /// <param name="commandName">�R�}���h�̖���</param>
        /// <param name="num">���l�ɗp����l</param>
        /// <param name="axis">���ɗp����l</param>
        /// <param name="capacity">���̃R�}���h���v����e��</param>
        public MainCommand(bool lock_m,bool lock_n,bool lock_c,
                          string commandName,int num,int axis,int capacity) 
        {
            lockMenber = lock_m;
            lockNumber = lock_n;
            lockCoordinateAxis = lock_c;
            this.commandName = commandName;
            this.num = new NumCommand(num);
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
            lockNumber = status.lockNumber;
            lockCoordinateAxis = status.lockCoordinateAxis;
            commandName = status.type.ToString();
            num = new NumCommand(status.num);
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
        /// �R�}���h���s�J�n���Ɏ��s���ׂ��֐�
        /// </summary>
        /// <param name="completeAction">�R�}���h�v���������Ɏ��s����A�N�V����</param>
        /// <param name="obj">�R�}���h�ɂ��ύX�𔽉f����I�u�W�F�N�g</param>
        public virtual void ActionActivate(Action completeAction,GameObject obj) 
        {
            this.completeAction = completeAction;
        }

        /// <summary>
        /// �R�}���h���s�֐�
        /// </summary>
        /// <param name="state">�ǂ��������󋵂œ�������</param>
        /// <param name="targetTransform">�Ώۂ̊e��l�ύX�ptransform</param>
        public virtual void CommandExecute(CommandState state, Transform targetTransform) { }

        /// <summary>
        /// �������擾����֐�
        /// </summary>
        /// <returns></returns>
        protected Vector3 GetVec()
        {
            Vector3 returnVec = Vector3.zero;
            switch (axis_local)
            {
                case CoordinateAxis.X: returnVec = Vector3.right; break;
                case CoordinateAxis.Y: returnVec = Vector3.up; break;
                case CoordinateAxis.Z: returnVec = Vector3.forward; break;
            }

            if (num_local < 0) returnVec *= -1;

            return returnVec;
        }

        /// <summary>
        /// �R�}���h�̖��̂��擾����֐�
        /// </summary>
        /// <returns></returns>
        public string NameGet()
        {
            return commandName;
        }

        /// <summary>
        /// ���l�̃R�}���h�e�L�X�g���擾����֐�
        /// </summary>
        /// <returns></returns>
        public string NumTextGet()
        {
            return num != null ? num.GetString() : default;
        }

        /// <summary>
        /// ���̃R�}���h�e�L�X�g���擾����֐�
        /// </summary>
        /// <returns></returns>
        public string AxisTextGet()
        {
            return axis != null ? axis.GetString() : default;
        }

        /// <summary>
        /// �R�}���h�N���X��null�łȂ������m�F����֐�
        /// </summary>
        /// <returns></returns>
        public bool CommandNullCheck()
        {
            return num != null && axis != null;
        }

        public override string GetString()
        {
            return $"{commandName} {(axis != null ? axis.GetString() : default)} {(num != null ? num.GetString() : default)}";
        }

        public override void StartUp()
        {
            num_local = num.numGet;
            axis_local = axis.axisGet;
        }

        public override CommandType ConfirmationCommandType()
        {
            return CommandType.Command;
        }

    }
}
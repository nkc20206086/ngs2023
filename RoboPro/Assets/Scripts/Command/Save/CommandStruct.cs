using UnityEngine;

namespace Command
{
    /// <summary>
    /// �R�}���h�������ɗ��p����\����
    /// </summary>
    [System.Serializable]
    public struct CommandStruct
    {
        [SerializeField,Tooltip("���̃R�}���h���ǂ̎�ނł��邩")]
        private MainCommandType CommandType;
        [SerializeField,Tooltip("�R�}���h���ړ��\�ł��邩")]
        private bool LockCommand;
        [SerializeField, Tooltip("�R�}���h�Ŏg�p���鐔�l")]
        private int Value;
        [SerializeField,Tooltip("�R�}���h���̐��l���ړ��\�ł��邩")]
        private bool LockNumber;
        [SerializeField, Tooltip("�R�}���h�Ŏg�p���鎲")]
        private CoordinateAxis Axis;
        [SerializeField,Tooltip("�R�}���h���̎����ړ��\�ł��邩")]
        private bool LockCoordinateAxis;

        // �e��Q�b�g�v���p�e�B
        public MainCommandType commandType { get => CommandType; } 
        public bool lockCommand { get => LockCommand; }
        public bool lockNumber { get => LockNumber; }
        public bool lockCoordinateAxis { get => LockCoordinateAxis; }
        public int value { get => Value; }
        public CoordinateAxis axis { get => Axis; }

        /// <summary>
        /// �R���X�g���N�^(�R���X�g���N�^�ɂ������ł̂ݕϐ���ύX�ł��܂�)
        /// </summary>
        /// <param name="commandType">���C���R�}���h�^�C�v</param>
        /// <param name="lockCommand">�R�}���h���ύX�\���ǂ���</param>
        /// <param name="lockNumber">���l���ύX�\���ǂ���</param>
        /// <param name="lockCoordinateAxis">����ύX�\���ǂ���</param>
        /// <param name="num">�p���鐔�l</param>
        /// <param name="axis">�p���鎲</param>
        public CommandStruct(MainCommandType commandType,
            bool lockCommand,bool lockNumber,bool lockCoordinateAxis,
            int num,CoordinateAxis axis,int capacity)
        {
            CommandType = commandType;
            LockCommand = lockCommand;
            LockNumber = lockNumber;
            LockCoordinateAxis = lockCoordinateAxis;
            Value = num;
            Axis = axis;
        }
    }

}
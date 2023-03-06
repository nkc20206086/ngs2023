namespace Command
{
    /// <summary>
    /// �R�}���h�������ɗ��p����\����
    /// </summary>
    public struct CommandStruct
    {
        public MainCommandType type { get; private set; }       // ���C���R�}���h�^�C�v

        public bool lockCommand { get; private set; }           // �R�}���h���ύX�\���ǂ���
        public bool lockNumber { get; private set; }            // ���l���ύX�\���ǂ���
        public bool lockCoordinateAxis { get; private set; }    // ����ύX�\���ǂ���
        public int num { get; private set; }                    // �p���鐔�l
        public CoordinateAxis axis { get; private set; }        // �p���鎲
        public int capacity { get; private set; }               // ���̃R�}���h�̗e��

        /// <summary>
        /// �R���X�g���N�^(�R���X�g���N�^�ɂ������ł̂ݕϐ���ύX�ł��܂�)
        /// </summary>
        /// <param name="type">���C���R�}���h�^�C�v</param>
        /// <param name="lockCommand">�R�}���h���ύX�\���ǂ���</param>
        /// <param name="lockNumber">���l���ύX�\���ǂ���</param>
        /// <param name="lockCoordinateAxis">����ύX�\���ǂ���</param>
        /// <param name="num">�p���鐔�l</param>
        /// <param name="axis">�p���鎲</param>
        /// <param name="capacity">���̃R�}���h�̗e��</param>
        public CommandStruct(MainCommandType type,
            bool lockCommand,bool lockNumber,bool lockCoordinateAxis,
            int num,CoordinateAxis axis,int capacity)
        {
            this.type = type;
            this.lockCommand = lockCommand;
            this.lockNumber = lockNumber;
            this.lockCoordinateAxis = lockCoordinateAxis;
            this.num = num;
            this.axis = axis;
            this.capacity = capacity;
        }
    }

}
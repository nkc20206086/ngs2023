namespace Player
{
    interface IStateGetter
    {
        /// <summary>
        /// ���݂̏�Ԃ��擾
        /// </summary>
        /// <returns></returns>
        public PlayerStateEnum StateGetter();

        /// <summary>
        /// �ړ��X�s�[�h���擾
        /// </summary>
        /// <returns></returns>
        public float SpeedGetter();
    }
}

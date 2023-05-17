using UnityEngine;

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

        /// <summary>
        /// �W�����v�̍����ƕ����擾
        /// </summary>
        /// <returns></returns>
        public Vector2 JumpPowerGetter();
    }
}

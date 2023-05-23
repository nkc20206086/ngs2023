using UnityEngine;

namespace Player
{
    interface IStateGetter
    {
        /// <summary>
        /// Animator���擾
        /// </summary>
        /// <returns></returns>
        public Animator PlayerAnimatorGeter();

        /// <summary>
        /// Rigidbody���擾
        /// </summary>
        /// <returns></returns>
        public Rigidbody rigidbodyGetter();

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
        /// ��q�̏�艺��̃X�s�[�h���擾
        /// </summary>
        /// <returns></returns>
        public float LadderUpDownSpeedGetter();

        /// <summary>
        /// �W�����v�̍����ƕ����擾
        /// </summary>
        /// <returns></returns>
        public Vector2 JumpPowerGetter();
    }
}

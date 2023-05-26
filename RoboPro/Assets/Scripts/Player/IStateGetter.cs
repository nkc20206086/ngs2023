using Gimmick.Interface;
using UnityEngine;

namespace Player
{
    public interface IStateGetter
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
        public Rigidbody RigidbodyGetter();

        /// <summary>
        /// ���݂̏�Ԃ��擾
        /// </summary>
        /// <returns></returns>
        public PlayerStateEnum StateGetter();

        /// <summary>
        /// �A�N�Z�X�|�C���g�̎擾
        /// </summary>
        /// <returns></returns>
        public IGimmickAccess GimmickAccessGetter();

        /// <summary>
        /// ������N���X�̎擾
        /// </summary>
        /// <returns></returns>
        public GroundChecker GroundCheckGetter();

        /// <summary>
        /// ��q����N���X�̎擾
        /// </summary>
        /// <returns></returns>
        public LadderChecker LadderCheckGetter();

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

        /// <summary>
        /// ���ʍ������擾
        /// </summary>
        /// <returns></returns>
        public float DeathHeightGetter();

        public float PlayerUI_OffsetYGetter();
    }
}

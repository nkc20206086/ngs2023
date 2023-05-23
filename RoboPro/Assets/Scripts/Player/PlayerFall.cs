using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFall : MonoBehaviour, IStateChange
    {
        private Rigidbody rigidbody;
        private Animator animator;
        private GroundChecker groundChecker;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector2 jumpVec;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            groundChecker = GetComponent<GroundChecker>();
            animator = GetComponentInChildren<Animator>();
            stateGetter = GetComponent<IStateGetter>();

            jumpVec = stateGetter.JumpPowerGetter();
        }

        /// <summary>
        /// �����֐�
        /// </summary>
        public void Act_Fall()
        {
            animator.SetBool("Flg_Fall", true);

            //��э~��邽�߂̏��W�����v
            rigidbody.velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y, transform.forward.z * jumpVec.x);

            //false��������󒆂ɂ���
            if(groundChecker.LandingCheck() == false)
            {
                //�󒆗������X�e�[�g�ɕύX
                stateChangeEvent(PlayerStateEnum.Falling);
            }
        }

        /// <summary>
        /// �ӂ���𖳎����������֐�
        /// </summary>
        public void Act_ThroughFall()
        {
            animator.SetBool("Flg_Fall", true);
            rigidbody.velocity = new Vector3(transform.forward.x * jumpVec.x, rigidbody.velocity.y, transform.forward.z * jumpVec.x);
            stateChangeEvent(PlayerStateEnum.Falling);
        }

        /// <summary>
        /// ������
        /// </summary>
        public void Act_Falling()
        {
            //true�ɂȂ�����n�ʂɒ��n���Ă���
            if (groundChecker.LandingCheck())
            {
                //���n�X�e�[�g�ɕύX
                stateChangeEvent(PlayerStateEnum.Landing);
            }
        }
    }
}
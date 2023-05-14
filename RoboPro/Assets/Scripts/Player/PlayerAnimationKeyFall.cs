using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationKeyFall : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;

        private bool isFalling = false;

        private Rigidbody rigidbody;
        private Animator animator;
        private PlayerCore playerCore;
        private PlayerFall playerFall;
        private GroundChecker groundChecker;

        private Vector2 jumpPower;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = player.GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            playerCore = player.GetComponent<PlayerCore>();
            playerFall = player.GetComponent<PlayerFall>();
            groundChecker = player.GetComponent<GroundChecker>();

            jumpPower = playerCore.JumpPowerGetter();
        }

        private void Update()
        {
            if (isFalling == false) return;
            //�������ɏ��ɕt�������𔻒�
            if (groundChecker.LandingCheck())
            {
                animator.SetBool("Flg_Fall", false);
                animator.SetBool("Flg_Landing", true);
                isFalling = false;
            }
        }

        /// <summary>
        /// �����J�n�̃A�j���[�V�����C�x���g
        /// </summary>
        public void Start_Fall()
        {
            //��э~��邽�߂̏��W�����v
            rigidbody.velocity = new Vector3(transform.forward.x * jumpPower.x, jumpPower.y, transform.forward.z * jumpPower.x);
            animator.SetBool("Flg_Fall", true);
        }

        /// <summary>
        /// �����A�j���[�V�������̃A�j���[�V�����C�x���g
        /// </summary>
        public void GroundCheck()
        {
            isFalling = true;
        }

        /// <summary>
        /// �����I���̃A�j���[�V�����C�x���g
        /// </summary>
        public void Finish_Fall()
        {
            playerFall.Finish_FallChange();
        }
    }
}

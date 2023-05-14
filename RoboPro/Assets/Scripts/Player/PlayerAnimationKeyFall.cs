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
            //落下中に床に付いたかを判定
            if (groundChecker.LandingCheck())
            {
                animator.SetBool("Flg_Fall", false);
                animator.SetBool("Flg_Landing", true);
                isFalling = false;
            }
        }

        /// <summary>
        /// 落下開始のアニメーションイベント
        /// </summary>
        public void Start_Fall()
        {
            //飛び降りるための小ジャンプ
            rigidbody.velocity = new Vector3(transform.forward.x * jumpPower.x, jumpPower.y, transform.forward.z * jumpPower.x);
            animator.SetBool("Flg_Fall", true);
        }

        /// <summary>
        /// 落下アニメーション中のアニメーションイベント
        /// </summary>
        public void GroundCheck()
        {
            isFalling = true;
        }

        /// <summary>
        /// 落下終了のアニメーションイベント
        /// </summary>
        public void Finish_Fall()
        {
            playerFall.Finish_FallChange();
        }
    }
}

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
        private PlayerCore playerCore;
        private GroundChecker groundChecker;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector2 jumpVec;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            playerCore = GetComponent<PlayerCore>();
            groundChecker = GetComponent<GroundChecker>();
            animator = GetComponentInChildren<Animator>();

            jumpVec = playerCore.JumpPowerGetter();
        }

        /// <summary>
        /// 落下関数
        /// </summary>
        public void Act_Fall()
        {
            animator.SetBool("Flg_Fall", true);

            //飛び降りるための小ジャンプ
            rigidbody.velocity = new Vector3(transform.forward.x * jumpVec.x, rigidbody.velocity.y, transform.forward.z * jumpVec.x);

            //falseだったら空中にいる
            if(groundChecker.LandingCheck() == false)
            {
                //空中落下中ステートに変更
                stateChangeEvent(PlayerStateEnum.Falling);
            }
        }

        /// <summary>
        /// ふらつきを無視した落下関数
        /// </summary>
        public void Act_ThroughFall()
        {
            animator.SetBool("Flg_Fall", true);
            rigidbody.velocity = new Vector3(transform.forward.x * jumpVec.x, rigidbody.velocity.y, transform.forward.z * jumpVec.x);
            stateChangeEvent(PlayerStateEnum.Falling);
        }

        /// <summary>
        /// 落下中
        /// </summary>
        public void Act_Falling()
        {
            //trueになったら地面に着地している
            if (groundChecker.LandingCheck())
            {
                //着地ステートに変更
                stateChangeEvent(PlayerStateEnum.Landing);
            }
        }
    }
}
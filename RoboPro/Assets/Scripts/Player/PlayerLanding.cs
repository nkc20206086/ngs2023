using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLanding : MonoBehaviour,IStateChange
    {
        private Animator animator;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        /// <summary>
        /// 着地関数
        /// </summary>
        public void Act_Landing()
        {
            animator.SetBool("Flg_Landing", true);
            animator.SetBool("Flg_Fall", false);
        }

        /// <summary>
        /// 着地終了関数
        /// </summary>
        public void Finish_Landing()
        {
            animator.SetBool("Flg_Landing", false);
            stateChangeEvent(PlayerStateEnum.Stay);
        }
    }

}
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
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private bool throughFallFlg;

        private Vector2 jumpVec;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            playerCore = GetComponent<PlayerCore>();
            groundChecker = GetComponent<GroundChecker>();
            animator = GetComponentInChildren<Animator>();
            stateGetter = GetComponent<IStateGetter>();

            jumpVec = playerCore.JumpPowerGetter();
        }

        /// <summary>
        /// óéâ∫ä÷êî
        /// </summary>
        public void Act_Fall()
        {
            animator.SetBool("Flg_StepOff", true);
            animator.SetBool("Flg_Cliff", false);
        }

        /// <summary>
        /// Ç”ÇÁÇ¬Ç´Çñ≥éãÇµÇΩóéâ∫ä÷êî
        /// </summary>
        public void Act_ThroughFall()
        {
            if (throughFallFlg) return;
            rigidbody.velocity = new Vector3(transform.forward.x * jumpVec.x, rigidbody.velocity.y, transform.forward.z * jumpVec.x);
            animator.SetBool("Flg_Fall", true);
            throughFallFlg = true;
        }

        /// <summary>
        /// óéâ∫èIóπä÷êî
        /// </summary>
        public void Finish_FallChange()
        {
            stateChangeEvent(PlayerStateEnum.Stay);
            throughFallFlg = false;
            animator.SetBool("Flg_Landing", false);
            animator.SetBool("Flg_StepOff", false);
        }
    }
}
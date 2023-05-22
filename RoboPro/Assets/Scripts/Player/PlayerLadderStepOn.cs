using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLadderStepOn : MonoBehaviour,IStateChange
    {
        private Rigidbody rigidbody;
        private Animator animator;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private bool startStepOnFlg = true;

        private Vector3 jumpVec;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            stateGetter = GetComponent<IStateGetter>();
            jumpVec = stateGetter.JumpPowerGetter();
        }

        public void Act_StepOn()
        {
            if (startStepOnFlg == false) return;
            rigidbody.velocity = new Vector3(transform.forward.x * jumpVec.x, rigidbody.velocity.y * jumpVec.y, transform.forward.z * jumpVec.x);
            animator.SetBool("Flg_Ladder_StepOn", true);
            startStepOnFlg = false;
        }

        public void Finish_StepOn()
        {
            animator.SetBool("Flg_Ladder_StepOn",false);
            stateChangeEvent(PlayerStateEnum.LaddderClimb);
            startStepOnFlg = true;
        }
    }

}
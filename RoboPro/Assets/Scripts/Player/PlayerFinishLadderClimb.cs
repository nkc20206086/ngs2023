using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFinishLadderClimb : MonoBehaviour,IStateChange
    {
        private CapsuleCollider capsuleCollider;
        private IStateGetter stateGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector3 jumpVec;
        private bool isGetOff;


        // Start is called before the first frame update
        void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            stateGetter = GetComponent<IStateGetter>();
            jumpVec = stateGetter.JumpPowerGetter();
        }

        public void Act_FinishClimb()
        {
            capsuleCollider.isTrigger = true;
            if (isGetOff == false)
            {
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Ladder_Climb", false);
                stateGetter.PlayerAnimatorGeter().SetTrigger("Trigger_JumpOff_Climb");
                stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y * 2, transform.forward.z * jumpVec.x);
                isGetOff = true;
            }
            
            if(stateGetter.RigidbodyGetter().velocity.y < 0)
            {
                isGetOff = false;
                capsuleCollider.isTrigger = false;
                stateChangeEvent(PlayerStateEnum.Stay);
            }
        }
    }
}


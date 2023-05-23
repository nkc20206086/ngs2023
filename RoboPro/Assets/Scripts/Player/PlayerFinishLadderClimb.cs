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
                stateGetter.rigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y * 2, transform.forward.z * jumpVec.x);
                Debug.Log("èIóπ");
                isGetOff = true;
            }
            
            if(stateGetter.rigidbodyGetter().velocity.y < 0)
            {
                isGetOff = false;
                capsuleCollider.isTrigger = false;
                stateChangeEvent(PlayerStateEnum.Stay);
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Ladder_Climb", false);
            }
        }
    }
}


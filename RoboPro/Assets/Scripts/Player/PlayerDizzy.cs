using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDizzy : MonoBehaviour,IStateChange
    {
        private Rigidbody rigidbody;
        private Animator animator;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Dizzy(bool isMove,bool isInteract)
        {
            if(isMove)
            {
                //Debug.Log("‚Ó‚ç‚Â‚«");
                animator.SetBool("Flg_Cliff", true);
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.Fall);
                }
            }
            else
            {
                animator.SetBool("Flg_Cliff", false);
                stateChangeEvent(PlayerStateEnum.Stay);
            }
        }
    }

}
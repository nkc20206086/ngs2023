using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStay : MonoBehaviour,IStateChange
    {
        private Rigidbody rigidbody;
        private GroundChecker groundChecker;
        private Animator animator;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            groundChecker = GetComponent<GroundChecker>();
            animator = GetComponentInChildren<Animator>();
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Stay(bool isMove, bool isInteract)
        {
            
            //Debug.Log("ë“Ç¬");
            if(isMove)
            {
                stateChangeEvent(PlayerStateEnum.Move);
            }

            if(isInteract)
            {
                stateChangeEvent(PlayerStateEnum.Access);
            }

            //è∞Ç…Ç¢ÇÈÇ©Ç«Ç§Ç©ÇîªíËÇ∑ÇÈ
            if (groundChecker.LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                animator.SetBool("Flg_Fall", true);
            }
        }
    }
}
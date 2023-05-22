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
        private LadderChecker ladderChecker;
        private Animator animator;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            groundChecker = GetComponent<GroundChecker>();
            ladderChecker = GetComponent<LadderChecker>();
            animator = GetComponentInChildren<Animator>();
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Stay(bool isMove, bool isInteract)
        {
            
            //Debug.Log("待つ");
            if(isMove)
            {
                stateChangeEvent(PlayerStateEnum.Move);
            }

            if(isInteract)
            {
                stateChangeEvent(PlayerStateEnum.Access);
            }

            Debug.Log(ladderChecker.LadderClimbCheck());
            Debug.Log(ladderChecker.LadderDownCheck());
            if(ladderChecker.LadderClimbCheck())
            {
                if(isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                }
            }

            if(ladderChecker.LadderDownCheck())
            {
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderDown);
                }
            }

            //床にいるかどうかを判定する
            if (groundChecker.LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                animator.SetBool("Flg_Fall", true);
            }
        }
    }
}
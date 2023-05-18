using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDizzy : MonoBehaviour,IStateChange
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

        public void Act_Dizzy(bool isMove,bool isInteract)
        {
            if(isMove)
            {
                //Debug.Log("�ӂ��");
                animator.SetBool("Flg_Cliff", true);
                if (isInteract)
                {
                   // Debug.Log("�~���");
                    stateChangeEvent(PlayerStateEnum.StepOff);
                }
            }
            else
            {
                animator.SetBool("Flg_Cliff", false);
                stateChangeEvent(PlayerStateEnum.Stay);
            }

            //���ɂ��邩�ǂ����𔻒肷��
            if (groundChecker.LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                animator.SetBool("Flg_Fall", true);
            }
        }
    }

}
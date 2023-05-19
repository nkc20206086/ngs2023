using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStepOff : MonoBehaviour,IStateChange
    {
        private Animator animator;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        /// <summary>
        /// îÚÇ—è„Ç™ÇË
        /// </summary>
        public void Act_StepOff()
        {
            animator.SetBool("Flg_StepOff", true);
            animator.SetBool("Flg_Cliff", false);
        }

        /// <summary>
        /// îÚÇ—è„Ç™ÇËèIóπ
        /// </summary>
        public void Finish_StepOff()
        {
            animator.SetBool("Flg_StepOff", false);
            stateChangeEvent(PlayerStateEnum.Fall);
        }
    }
}
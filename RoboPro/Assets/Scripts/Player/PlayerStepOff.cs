using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStepOff : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        /// <summary>
        /// ”ò‚Ñã‚ª‚è
        /// </summary>
        public void Act_StepOff()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_StepOff", true);
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Cliff", false);
        }

        /// <summary>
        /// ”ò‚Ñã‚ª‚èI—¹
        /// </summary>
        public void Finish_StepOff()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_StepOff", false);
            stateChangeEvent(PlayerStateEnum.Fall);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLanding : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponentInChildren<IStateGetter>();
        }

        /// <summary>
        /// ’…’nŠÖ”
        /// </summary>
        public void Act_Landing()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Landing", true);
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", false);
        }

        /// <summary>
        /// ’…’nI—¹ŠÖ”
        /// </summary>
        public void Finish_Landing()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Landing", false);
            stateChangeEvent(PlayerStateEnum.Stay);
        }
    }

}
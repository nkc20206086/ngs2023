using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLadderClimb : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Climb()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Ladder_Climb", true);
            stateGetter.RigidbodyGetter().velocity = new Vector3(0, stateGetter.LadderUpDownSpeedGetter(), 0);

            if (stateGetter.LadderCheckGetter().Complete_LadderClimbCheck()) return;
            stateChangeEvent(PlayerStateEnum.LadderFinish_Climb);
        }
    }
}
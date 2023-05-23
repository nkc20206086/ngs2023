using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLadderClimb : MonoBehaviour,IStateChange
    {
        private Rigidbody rigidbody;
        private Animator animator;
        private IStateGetter stateGetter;
        private LadderChecker ladderChecker;

        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            stateGetter = GetComponent<IStateGetter>();
            ladderChecker = GetComponent<LadderChecker>();
        }

        public void Act_Climb()
        {
            animator.SetBool("Flg_Ladder_Climb", true);
            rigidbody.velocity = new Vector3(0, stateGetter.LadderUpDownSpeedGetter(), 0);

            Debug.Log(ladderChecker.Complete_LadderClimbCheck());
            if (ladderChecker.Complete_LadderClimbCheck()) return;
            stateChangeEvent(PlayerStateEnum.LadderFinish_Climb);
        }
    }
}
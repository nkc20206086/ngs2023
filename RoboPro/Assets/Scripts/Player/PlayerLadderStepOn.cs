using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLadderStepOn : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private bool startStepOnFlg = true;

        private Vector3 jumpVec;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
            jumpVec = stateGetter.JumpPowerGetter();
        }

        public void Act_StepOn()
        {
            if (startStepOnFlg == false) return;
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y, transform.forward.z * jumpVec.x);
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Ladder_StepOn", true);
            startStepOnFlg = false;
        }

        public void Finish_StepOn()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Ladder_StepOn",false);
            stateChangeEvent(PlayerStateEnum.LaddderClimb);
            startStepOnFlg = true;
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationKey : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;

        private PlayerStepOff playerStepOff;
        private PlayerLanding playerLanding;
        private PlayerLadderStepOn playerLadderStepOn;
        private PlayerGoalJump playerGoal;

        // Start is called before the first frame update
        void Start()
        {
            playerStepOff = player.GetComponent<PlayerStepOff>();
            playerLanding = player.GetComponent<PlayerLanding>();
            playerLadderStepOn = player.GetComponent<PlayerLadderStepOn>();
            playerGoal = player.GetComponent<PlayerGoalJump>();
        }

        /// <summary>
        /// 飛び上がり終了のアニメーションイベント
        /// </summary>
        public void Finish_StepOff_AnimationKey()
        {
            playerStepOff.Finish_StepOff();
        }

        /// <summary>
        /// 落下終了のアニメーションイベント
        /// </summary>
        public void Finish_Landing_AnimationKey()
        {
            playerLanding.Finish_Landing();
        }

        /// <summary>
        /// 登る時の梯子に掴まるAnimationの終了アニメーションイベント
        /// </summary>
        public void Finish_LadderStepOnClimb_AnimatinKey()
        {
            playerLadderStepOn.Finish_StepOn();
        }

        public void Finish_GoalJump_AnimatinKey()
        {
            playerGoal.Finish_GoTo_Goal();
        }

        public void Finish_GoalDance_AnimatinKey()
        {

        }
    }
}

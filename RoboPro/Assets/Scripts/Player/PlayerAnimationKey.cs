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

        [SerializeField]
        private GameObject goalCrackerEffect;

        [SerializeField]
        private GameObject explosionEffect;

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
            Instantiate(goalCrackerEffect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z), Quaternion.identity);
            goalCrackerEffect.gameObject.SetActive(true);
        }

        public void Death_Explosion_AnimationKey()
        {
            Instantiate(explosionEffect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z), Quaternion.identity);
            explosionEffect.layer = 7;
            explosionEffect.gameObject.SetActive(true);
        }
    }
}

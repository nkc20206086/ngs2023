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

        // Start is called before the first frame update
        void Start()
        {
            playerStepOff = player.GetComponent<PlayerStepOff>();
            playerLanding = player.GetComponent<PlayerLanding>();
        }

        /// <summary>
        /// ��яオ��I���̃A�j���[�V�����C�x���g
        /// </summary>
        public void Finish_StepOff_AnimationKey()
        {
            playerStepOff.Finish_StepOff();
        }

        /// <summary>
        /// �����I���̃A�j���[�V�����C�x���g
        /// </summary>
        public void Finish_Landing_AnimationKey()
        {
            playerLanding.Finish_Landing();
        }
    }
}

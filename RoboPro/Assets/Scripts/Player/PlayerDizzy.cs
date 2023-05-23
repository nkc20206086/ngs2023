using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDizzy : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;


        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Dizzy(bool isMove,bool isInteract)
        {
            if(isMove)
            {
                //Debug.Log("�ӂ��");
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Cliff", true);
                if (isInteract)
                {
                   // Debug.Log("�~���");
                    stateChangeEvent(PlayerStateEnum.StepOff);
                }
            }
            else
            {
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Cliff", false);
                stateChangeEvent(PlayerStateEnum.Stay);
            }

            //���ɂ��邩�ǂ����𔻒肷��
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            }
        }
    }

}
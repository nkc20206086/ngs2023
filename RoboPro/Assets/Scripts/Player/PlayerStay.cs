using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStay : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Stay(bool isMove, bool isInteract)
        {
            
            //Debug.Log("�҂�");
            if(isMove)
            {
                stateChangeEvent(PlayerStateEnum.Move);
            }

            if (stateGetter.LadderCheckGetter().LadderClimbCheck())
            {
                if(isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                }
            }

            if(stateGetter.LadderCheckGetter().LadderDownCheck())
            {
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderDown);
                }
            }

            //int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);

            //if (index >= 0)
            //{
            //    if (isInteract)
            //    {
            //        stateChangeEvent(PlayerStateEnum.Access);
            //        stateGetter.GimmickAccessGetter().Access(index);
            //        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Access", true);
            //    }
            //}

            //���ɂ��邩�ǂ����𔻒肷��
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            }
        }
    }
}
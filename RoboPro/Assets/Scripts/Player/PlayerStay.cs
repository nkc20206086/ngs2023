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
        Vector3 defaultScale;

        // Start is called before the first frame update
        void Start()
        {
            defaultScale = transform.lossyScale;
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Stay(bool isMove, bool isInteract)
        {
            
            //Debug.Log("待つ");
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

            //アクセスポイントの何番が近くにあるか
            int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
            if (index >= 0)
            {
                if (isInteract)
                {
                    //アクセスポイントに接続する
                    Vector3 pos = stateGetter.GimmickAccessGetter().Access(index);
                    pos.y = this.transform.position.y;
                    transform.LookAt(pos);

                    stateChangeEvent(PlayerStateEnum.Access);
                }
            }

            //床にいるかどうかを判定する
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStay : MonoBehaviour,IStateChange
    {
        private Goal goal;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            goal = GameObject.FindObjectOfType<Goal>();
            goal.OnStartInteract += Act_GoalPoint;

            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Stay(bool isMove, bool isInteract)
        {
            //stateChangeEvent(PlayerStateEnum.Goal_Jump);

            //Debug.Log("待つ");
            if (isMove)
            {
                stateChangeEvent(PlayerStateEnum.Move);
            }

            //if (stateGetter.LadderCheckGetter().LadderClimbCheck())
            //{
            //    if(isInteract)
            //    {
            //        stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
            //    }
            //}

            //if(stateGetter.LadderCheckGetter().LadderDownCheck())
            //{
            //    if (isInteract)
            //    {
            //        stateChangeEvent(PlayerStateEnum.LadderDown);
            //    }
            //}

            //アクセスポイントの何番が近くにあるか
            int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
            if (index >= 0)
            {
                if (isInteract)
                {
                    bool access = stateGetter.GimmickAccessGetter().Access(index);
                    if (access)
                    {
                        //アクセスポイントに接続する
                        Vector3 pos = stateGetter.GimmickAccessGetter().GetPosition(index);
                        pos.y = this.transform.position.y;
                        transform.LookAt(pos);

                        stateChangeEvent(PlayerStateEnum.Access);
                    }
                }
            }

            //床にいるかどうかを判定する
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            }
        }

        private void Act_GoalPoint()
        {
            Vector3 pos = goal.gameObject.transform.position;
            pos.y = this.transform.position.y;
            transform.LookAt(pos);
            stateChangeEvent(PlayerStateEnum.Access);
        }
    }
}
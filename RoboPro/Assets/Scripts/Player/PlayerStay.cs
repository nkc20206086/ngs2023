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
            if (isMove)
            {
                stateChangeEvent(PlayerStateEnum.Move);
            }


            Ladder_Check(isInteract);
            AccessPoint_Check(isInteract);

            Dizzy_Check(isInteract);

            //床にいるかどうかを判定する
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
        }

        /// <summary>
        /// 梯子が近くにあるかどうか
        /// </summary>
        /// <param name="isInteract"></param>
        private void Ladder_Check(bool isInteract)
        {
            if (stateGetter.LadderCheckGetter().LadderClimbCheck())
            {
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                }
            }

            if (stateGetter.LadderCheckGetter().LadderDownCheck())
            {
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderDown);
                }
            }
        }

        /// <summary>
        /// アクセスポイントの何番が近くにあるか
        /// </summary>
        /// <param name="isInteract"></param>
        private void AccessPoint_Check(bool isInteract)
        {
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
        }

        private void Act_GoalPoint()
        {
            Vector3 pos = goal.gameObject.transform.position;
            pos.y = this.transform.position.y;
            transform.LookAt(pos);
            stateChangeEvent(PlayerStateEnum.Access);
        }

        private void Dizzy_Check(bool isInteract)
        {
            if (isInteract == false) return;
            if (stateGetter.GroundCheckGetter().CheckGround() == false)
            {
                //自分の乗っている床でふらつけるかどうかの判定
                if (stateGetter.GroundCheckGetter().DizzyGroundFlg())
                {
                    //ふらつくステートに変更
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Dizzy);
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;
using Zenject;

namespace Player
{
    public class PlayerMove : MonoBehaviour, IStateChange
    {
        [Inject]
        private ICameraVectorGetter cameraVectorGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;
        private IStateGetter stateGetter;
        private Goal goal;
        private GroundColliCheck colliCheck;

        private Vector3 moveForward;
        private GameObject moveEffect;

        // Start is called before the first frame update
        void Start()
        {
            colliCheck = GetComponent<GroundColliCheck>();
            stateGetter = GetComponent<IStateGetter>();

            moveEffect = transform.GetChild(1).gameObject;

            goal = GameObject.FindObjectOfType<Goal>();
            goal.OnStartInteract += Act_GoalPoint;
        }

        /// <summary>
        /// 移動関数
        /// </summary>
        /// <param name="isMove"></param>
        /// <param name="isInteract"></param>
        public void Act_Move(bool isMove, bool isInteract, Vector2 vec)
        {
            stateGetter.GroundCheckGetter().CheckWall();
            //床にいるかどうかを判定する
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                //Move中に落下しているということはふらつきを無視している　→　ThroughFall
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
            else
            {
                moveForward = cameraVectorGetter.VectorYGetter() * vec.y + cameraVectorGetter.VectorXGetter() * vec.x;
                moveForward = moveForward.normalized;

                if (isMove)
                {
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", true);
                    moveEffect.SetActive(true);
                    transform.LookAt(transform.position + moveForward);
                    stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * stateGetter.SpeedGetter(), stateGetter.RigidbodyGetter().velocity.y, transform.forward.z * stateGetter.SpeedGetter());
                }
                else
                {
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Stay);
                }
                
                ////登る梯子の検知
                //if (stateGetter.LadderCheckGetter().LadderClimbCheck())
                //{
                //    if (isInteract)
                //    {
                //        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                //        playerEffect.moveEffect.SetActive(false);
                //        stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                //    }
                //}

                ////下る梯子の検知
                //if (stateGetter.LadderCheckGetter().LadderDownCheck())
                //{
                //    if (isInteract)
                //    {
                //        stateChangeEvent(PlayerStateEnum.LadderDown);
                //    }
                //}

                //アクセスポイントの何番が近くにあるか
                int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
                //Debug.Log(index);
                if (index >= 0)
                {
                    if (isInteract)
                    {
                        bool access = stateGetter.GimmickAccessGetter().Access(index);
                        if (access)
                        {
                            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                            stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                            //アクセスポイントに接続する
                            Vector3 pos = stateGetter.GimmickAccessGetter().GetPosition(index);
                            pos.y = this.transform.position.y;
                            transform.LookAt(pos);

                            stateChangeEvent(PlayerStateEnum.Access);
                        }
                    }
                }

                //目の前が崖か判定
                if (stateGetter.GroundCheckGetter().CheckGround() == false)
                {
                    if(stateGetter.LadderCheckGetter().LadderClimbCheck() || stateGetter.LadderCheckGetter().LadderDownCheck())
                    {
                        stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    }
                    //自分の乗っている床でふらつけるかどうかの判定
                    else if (stateGetter.GroundCheckGetter().DizzyGroundFlg())
                    {
                        //ふらつくステートに変更
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                        stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                        //colliCheck.ColiCheck();
                        stateChangeEvent(PlayerStateEnum.Dizzy);
                    }
                }
            }
        }
        private void Act_GoalPoint()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("PlayerMove", false);
            Vector3 pos = goal.gameObject.transform.position;
            pos.y = this.transform.position.y;
            transform.LookAt(pos);
            stateChangeEvent(PlayerStateEnum.Access);
        }
    }
}
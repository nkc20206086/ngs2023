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
            if (stateGetter.GroundCheckGetter().LandingCheck())
            {
                //カメラの角度から移動ベクトルを補正
                moveForward = cameraVectorGetter.VectorYGetter() * vec.y + cameraVectorGetter.VectorXGetter() * vec.x;
                moveForward = moveForward.normalized;

                if (isMove)
                {
                    //移動する
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", true);
                    moveEffect.SetActive(true);
                    transform.LookAt(transform.position + moveForward);
                    stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * stateGetter.SpeedGetter(), stateGetter.RigidbodyGetter().velocity.y, transform.forward.z * stateGetter.SpeedGetter());
                }
                else
                {
                    //Stay状態にする
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Stay);
                }
                AccesPoint_Check(isInteract);
                Dizzy_Check();
            }
            else
            {
                //Move中に落下しているということはふらつきを無視している　→　ThroughFall
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
        }

        /// <summary>
        /// アクセスポイントの何番が近くにあるか
        /// </summary>
        private void AccesPoint_Check(bool isInteract)
        {
            int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
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
        }

        /// <summary>
        ///         //目の前が崖か判定
        /// </summary>
        private void Dizzy_Check()
        {
            if (stateGetter.GroundCheckGetter().CheckGround() == false)
            {
                //自分の乗っている床でふらつけるかどうかの判定
                if (stateGetter.GroundCheckGetter().DizzyGroundFlg())
                {
                    //ふらつくステートに変更
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Dizzy);
                }
            }
            else
            {
                if (SideCheck() == false)
                {
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                }
            }
        }

        private bool SideCheck()
        {
            if (stateGetter.GroundCheckGetter().CheckSideGround_Left() && stateGetter.GroundCheckGetter().CheckSideGround_Right())
            {
                return true;
            }
            else
            {
                return false;
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
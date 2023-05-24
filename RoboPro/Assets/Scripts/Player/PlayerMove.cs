using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;

namespace Player
{
    public class PlayerMove : MonoBehaviour, IStateChange
    {
        private GroundColliCheck colliCheck;
        private IStateGetter stateGetter;
        private ICameraVectorGetter cameraVectorGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector3 moveForward;
        [SerializeField]
        private GameObject camera;

        // Start is called before the first frame update
        void Start()
        {
            colliCheck = GetComponent<GroundColliCheck>();
            stateGetter = GetComponent<IStateGetter>();
            cameraVectorGetter = camera.GetComponent<ICameraVectorGetter>();
        }

        /// <summary>
        /// 移動関数
        /// </summary>
        /// <param name="isMove"></param>
        /// <param name="isInteract"></param>
        public void Act_Move(bool isMove, bool isInteract, Vector2 vec)
        {
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
                    transform.LookAt(transform.position + moveForward);
                    stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * stateGetter.SpeedGetter(), stateGetter.RigidbodyGetter().velocity.y, transform.forward.z * stateGetter.SpeedGetter());
                }
                else
                {
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Stay);
                }
                
                //登る梯子の検知
                if (stateGetter.LadderCheckGetter().LadderClimbCheck())
                {
                    if (isInteract)
                    {
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                        stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                    }
                }

                //下る梯子の検知
                if (stateGetter.LadderCheckGetter().LadderDownCheck())
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
                        transform.LookAt(stateGetter.GimmickAccessGetter().Access(index));
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                        stateChangeEvent(PlayerStateEnum.Access);
                    }
                }

                //目の前が崖か判定
                if (stateGetter.GroundCheckGetter().CheckGround(moveForward) == false)
                {
                    if(stateGetter.LadderCheckGetter().LadderClimbCheck() || stateGetter.LadderCheckGetter().LadderDownCheck())
                    {
                        stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    }
                    //自分の乗っている床でふらつけるかどうかの判定
                    else if (stateGetter.GroundCheckGetter().DizzyGroundFlg() == false)
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
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;

namespace Player
{
    public class PlayerMove : MonoBehaviour,IStateChange
    {
        private Rigidbody rigidbody;
        private Animator animator;
        private GroundChecker groundChecker;
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
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            groundChecker = GetComponent<GroundChecker>();
            colliCheck = GetComponent<GroundColliCheck>();
            stateGetter = GetComponent<IStateGetter>();
            cameraVectorGetter = camera.GetComponent<ICameraVectorGetter>();
        }

        /// <summary>
        /// 移動関数
        /// </summary>
        /// <param name="isMove"></param>
        /// <param name="isInteract"></param>
        public void Act_Move(bool isMove, bool isInteract,Vector2 vec)
        {
            //床にいるかどうかを判定する
            if (groundChecker.LandingCheck() == false)
            {
                //Move中に落下しているということはふらつきを無視している　→　ThroughFall
                animator.SetBool("Flg_Walk", false);
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
            else
            {
                moveForward = cameraVectorGetter.VectorYGetter() * vec.y + cameraVectorGetter.VectorXGetter() * vec.x;
                moveForward = moveForward.normalized;

                if (isMove)
                {
                    animator.SetBool("Flg_Walk", true);
                    transform.LookAt(transform.position + moveForward);
                    rigidbody.velocity = new Vector3(transform.forward.x * stateGetter.SpeedGetter(), rigidbody.velocity.y, transform.forward.z * stateGetter.SpeedGetter());
                }
                else
                {
                    animator.SetBool("Flg_Walk", false);
                    rigidbody.velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Stay);
                }

                //目の前が崖か判定
                if (groundChecker.CheckGround(moveForward) == false)
                {
                    //自分の乗っている床でふらつけるかどうかの判定
                    if (groundChecker.DizzyGroundFlg() == false)
                    {
                        //ふらつくステートに変更
                        animator.SetBool("Flg_Walk", false);
                        rigidbody.velocity = Vector3.zero;
                        //colliCheck.ColiCheck();
                        stateChangeEvent(PlayerStateEnum.Dizzy);
                    }
                }
            }
        }
    }
}
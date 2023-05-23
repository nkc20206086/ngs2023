using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFall : MonoBehaviour, IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector2 jumpVec;
        private float startFallYVector;
        private bool isThroughFall;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();

            jumpVec = stateGetter.JumpPowerGetter();
        }

        /// <summary>
        /// 落下関数
        /// </summary>
        public void Act_Fall()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);

            //飛び降りるための小ジャンプ
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y, transform.forward.z * jumpVec.x);

            //falseだったら空中にいる
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                //空中落下中ステートに変更
                stateChangeEvent(PlayerStateEnum.Falling);
            }
        }

        /// <summary>
        /// ふらつきを無視した落下関数
        /// </summary>
        public void Act_ThroughFall()
        {
            startFallYVector = transform.position.y;
            isThroughFall = true;
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, stateGetter.RigidbodyGetter().velocity.y, transform.forward.z * jumpVec.x);
            stateChangeEvent(PlayerStateEnum.Falling);
        }

        /// <summary>
        /// 落下中
        /// </summary>
        public void Act_Falling()
        {
            //trueになったら地面に着地している
            if (stateGetter.GroundCheckGetter().LandingCheck())
            {
                if(isThroughFall)
                {
                    float fallingYVector = startFallYVector - transform.position.y;
                    isThroughFall = false;
                    if (stateGetter.DeathHeigthGetter() < fallingYVector)
                    {
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", false);
                        stateChangeEvent(PlayerStateEnum.Die);
                    }
                    else
                    {
                        stateChangeEvent(PlayerStateEnum.Landing);
                    }
                }
                else
                { 
                    stateChangeEvent(PlayerStateEnum.Landing);
                }

            }
        }
    }
}
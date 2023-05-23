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
            if(stateGetter.GroundCheckGetter().LandingCheck() == false)
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
                //着地ステートに変更
                stateChangeEvent(PlayerStateEnum.Landing);
            }
        }
    }
}
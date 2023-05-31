using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerGoalJump : MonoBehaviour,IStateChange
    {
        private Animation animation;
        private AnimationState animationState;

        private int animatorLayer;
        private float distanceVec;
        float speed = 1f;
        private IStateGetter stateGetter;
        private Goal goal;

        Vector3 jumpVec;
        private bool isJump = false;
        private bool isVectorCalc;

        public event Action<PlayerStateEnum> stateChangeEvent;

        void Start()
        {
            animation = GetComponent<Animation>();
            stateGetter = GetComponent<IStateGetter>();
            goal = GameObject.FindObjectOfType<Goal>();

            jumpVec = stateGetter.JumpPowerGetter();
        }

        private void Act_GoalJump()
        {
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y * 2f, transform.forward.z * jumpVec.x);
        }

        public void Act_GoTo_Goal()
        {
            if(isVectorCalc == false)
            {
                //二点間の距離を代入(スピード調整に使う)
                distanceVec = Vector3.Distance(gameObject.transform.position, goal.gameObject.transform.position);
                float middleVector = distanceVec / 2;
                stateGetter.PlayerAnimatorGeter().SetTrigger("Trigger_GoalJump");
                Act_GoalJump();

                isVectorCalc = true;
            }

            // 現在の位置
            transform.position = Vector3.MoveTowards(gameObject.transform.position, goal.gameObject.transform.position, 1f * Time.deltaTime);
            //float currentPos = (Time.deltaTime * speed) / distanceVec;
            //transform.position = Vector3.Lerp(transform.position, goal.gameObject.transform.position, currentPos);


            Vector3 cameraPos = GoalCameraPositionGetter.GetPosition;
            cameraPos.y = gameObject.transform.position.y;
            transform.LookAt(cameraPos);
        }

        public void Finish_GoTo_Goal()
        {
            stateChangeEvent(PlayerStateEnum.Goal_Dance);
        }
    }

}

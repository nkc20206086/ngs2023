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

        public event Action<PlayerStateEnum> stateChangeEvent;

        void Start()
        {
            animation = GetComponent<Animation>();
            stateGetter = GetComponent<IStateGetter>();
            goal = GameObject.FindObjectOfType<Goal>();

            jumpVec = stateGetter.JumpPowerGetter();
            //二点間の距離を代入(スピード調整に使う)
            distanceVec = Vector3.Distance(transform.position, goal.gameObject.transform.position);
        }

        private void Act_GoalJump()
        {
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y * 2f, transform.forward.z * jumpVec.x);
        }

        public void Act_GoTo_Goal()
        {
            stateGetter.PlayerAnimatorGeter().SetTrigger("Trigger_GoalJump");
            // 現在の位置
            float currentPos = (Time.deltaTime * speed) / distanceVec;
            transform.position = Vector3.Lerp(transform.position, goal.gameObject.transform.position, currentPos);
            if (isJump) return;
            Act_GoalJump();
            isJump = true;
        }

        public void Finish_GoTo_Goal()
        {
            stateChangeEvent(PlayerStateEnum.Goal_Dance);
        }
    }

}

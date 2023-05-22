using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCore : MonoBehaviour,IStateGetter
    {
        private Animator animator;
        private Rigidbody rigidbody;
        private IStateChange[] stateChangeArray = new IStateChange[(int)PlayerStateEnum.Count];
        private PlayerStateEnum state;

        [SerializeField]
        private float ladderUpDownSpeed;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private Vector2 jumpPower;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            stateChangeArray = GetComponents<IStateChange>();
            foreach(IStateChange stateChange in stateChangeArray)
            {
                stateChange.stateChangeEvent += StateChanger;
            }
        }

        /// <param name="newStateEnum"></param>
        public void StateChanger(PlayerStateEnum newStateEnum)
        {
            //ステートを変更
            state = newStateEnum;
            //Debug.Log(state + "ステートに変更されました");
        }

        PlayerStateEnum IStateGetter.StateGetter()
        {
            return state;
        }

        float IStateGetter.SpeedGetter()
        {
            return moveSpeed;
        }

        public Vector2 JumpPowerGetter()
        {
            return jumpPower;
        }

        public float LadderUpDownSpeed()
        {
            return ladderUpDownSpeed;
        }

        public Animator PlayerAnimatorGeter()
        {
            return animator;
        }

        public Rigidbody rigidbodyGetter()
        {
            return rigidbody;
        }
    }
}
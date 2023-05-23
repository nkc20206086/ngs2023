using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCore : MonoBehaviour,IStateGetter
    {
        [SerializeField]
        private float ladderUpDownSpeed;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private Vector2 jumpPower;
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public Rigidbody rigidbody;

        private IStateChange[] stateChangeArray = new IStateChange[(int)PlayerStateEnum.Count];
        private PlayerStateEnum state;

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

        Animator IStateGetter.PlayerAnimatorGeter()
        {
            return animator;
        }

        Rigidbody IStateGetter.rigidbodyGetter()
        {
            return rigidbody;
        }

        float IStateGetter.LadderUpDownSpeedGetter()
        {
            return ladderUpDownSpeed;
        }

        Vector2 IStateGetter.JumpPowerGetter()
        {
            return jumpPower;
        }
    }
}
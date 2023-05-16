using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCore : MonoBehaviour,IStateGetter
    {
        private Rigidbody rigidbody;
        private IStateChange[] stateChangeArray = new IStateChange[(int)PlayerStateEnum.Count];
        private PlayerStateEnum state;
        public float moveSpeed;

        // Start is called before the first frame update
        void Start()
        {
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
            Debug.Log(state + "ステートに変更されました");
        }

        PlayerStateEnum IStateGetter.StateGetter()
        {
            return state;
        }

        float IStateGetter.SpeedGetter()
        {
            return moveSpeed;
        }
    }
}
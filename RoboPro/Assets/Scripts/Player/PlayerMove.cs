using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour,IStateChange
    {
        private Rigidbody rigidbody;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            stateGetter = GetComponent<IStateGetter>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// à⁄ìÆä÷êî
        /// </summary>
        /// <param name="isMove"></param>
        /// <param name="isInteract"></param>
        public void Act_Move(bool isMove, bool isInteract)
        {
            //Debug.Log("ìÆÇ≠");

            if(isMove == false)
            {
                stateChangeEvent(PlayerStateEnum.Stay);
            }

            if(isInteract)
            {
                stateChangeEvent(PlayerStateEnum.Dizzy);
            }
        }
    }
}
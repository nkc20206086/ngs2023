using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFall : MonoBehaviour,IStateChange
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

        /// <summary>
        /// —‰ºŠÖ”
        /// </summary>
        public void Act_Fall()
        {
            //Debug.Log("—‚¿‚é");

            stateChangeEvent(PlayerStateEnum.Stay);
        }
    }
}
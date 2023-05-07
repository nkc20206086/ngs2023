using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDizzy : MonoBehaviour,IStateChange
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

        public void Act_Dizzy(bool isMove,bool isInteract)
        {
            if(isMove)
            {
                //Debug.Log("‚Ó‚ç‚Â‚«");
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.Fall);
                }
            }
            else
            {
                stateChangeEvent(PlayerStateEnum.Stay);
            }

            

            
        }
    }

}
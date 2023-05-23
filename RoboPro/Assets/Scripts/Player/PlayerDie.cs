using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDie : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Die()
        {
            stateGetter.PlayerAnimatorGeter().SetTrigger("Trigger_Die");
        }
    }
}
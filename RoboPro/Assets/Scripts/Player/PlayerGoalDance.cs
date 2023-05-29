using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerGoalDance : MonoBehaviour
    {
        private IStateGetter stateGetter;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_GoalDance()
        {
            if (stateGetter.PlayerAnimatorGeter().GetBool("Trigger_GoalDance")) return;
            stateGetter.PlayerAnimatorGeter().SetTrigger("Trigger_GoalDance");
        }
    }

}
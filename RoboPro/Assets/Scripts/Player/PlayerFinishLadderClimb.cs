using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFinishLadderClimb : MonoBehaviour
    {
        private PlayerCore playerCore;
        // Start is called before the first frame update
        void Start()
        {
            playerCore = GetComponent<PlayerCore>();
        }

        public void Act_FinishClimb()
        {
            //playerCore.rigidbodyGetter() = new Vector3(transform.forward.x * jumpVec.x, rigidbody.velocity.y * jumpVec.y, transform.forward.z * jumpVec.x);
        }
    }
}


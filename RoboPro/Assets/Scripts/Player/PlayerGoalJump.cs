using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerGoalJump : MonoBehaviour
    {
        [SerializeField]
        GameObject goalObj;
        private float distanceVec;
        float speed = 1;
        private IStateGetter stateGetter;

        Vector3 jumpVec;
        private bool isJump = false;

        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();

            jumpVec = stateGetter.JumpPowerGetter();
            //��_�Ԃ̋�������(�X�s�[�h�����Ɏg��)
            //distanceVec = Vector3.Distance(transform.position, goalObj.transform.position);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Act_GoalJump()
        {
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y * 2f, transform.forward.z * jumpVec.x);
        }

        public void Go_Goal()
        {
            // ���݂̈ʒu
            float currentPos = (Time.time * speed) / distanceVec;
            transform.position = Vector3.Lerp(transform.position, goalObj.transform.position, currentPos);
            if (isJump) return;
            Act_GoalJump();
            isJump = true;
        }
    }

}

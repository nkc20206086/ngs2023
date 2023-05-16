using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;

namespace Player
{
    public class PlayerMove : MonoBehaviour,IStateChange
    {
        private Rigidbody rigidbody;
        private Animator animator;
        private GroundChecker groundChecker;
        private GroundColliCheck colliCheck;
        private IStateGetter stateGetter;
        private ICameraVectorGetter cameraVectorGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector3 moveForward;
        [SerializeField]
        private GameObject camera;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            groundChecker = GetComponent<GroundChecker>();
            colliCheck = GetComponent<GroundColliCheck>();
            stateGetter = GetComponent<IStateGetter>();
            cameraVectorGetter = camera.GetComponent<ICameraVectorGetter>();
        }

        /// <summary>
        /// à⁄ìÆä÷êî
        /// </summary>
        /// <param name="isMove"></param>
        /// <param name="isInteract"></param>
        public void Act_Move(bool isMove, bool isInteract,Vector2 vec)
        {
            //Debug.Log("ìÆÇ≠");
            moveForward = cameraVectorGetter.VectorYGetter() * vec.y + cameraVectorGetter.VectorXGetter() * vec.x;
            moveForward = moveForward.normalized;

            if (isMove)
            {
                animator.SetBool("Flg_Walk", true);
                transform.LookAt(transform.position + moveForward);
                rigidbody.velocity = new Vector3(transform.forward.x * stateGetter.SpeedGetter(), rigidbody.velocity.y, transform.forward.z * stateGetter.SpeedGetter());
            }
            else
            {
                animator.SetBool("Flg_Walk", false);
                rigidbody.velocity = Vector3.zero;
                stateChangeEvent(PlayerStateEnum.Stay);
            }

            if(groundChecker.CheckGround(moveForward) == false)
            {
                animator.SetBool("Flg_Walk", false);
                rigidbody.velocity = Vector3.zero;
                colliCheck.ColiCheck();
                stateChangeEvent(PlayerStateEnum.Dizzy);
            }
        }
    }
}
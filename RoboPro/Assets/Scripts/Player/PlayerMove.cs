using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;

namespace Player
{
    public class PlayerMove : MonoBehaviour, IStateChange
    {
        private Rigidbody rigidbody;
        private Animator animator;
        private GroundChecker groundChecker;
        private LadderChecker ladderChecker;
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
            ladderChecker = GetComponent<LadderChecker>();
            colliCheck = GetComponent<GroundColliCheck>();
            stateGetter = GetComponent<IStateGetter>();
            cameraVectorGetter = camera.GetComponent<ICameraVectorGetter>();
        }

        /// <summary>
        /// �ړ��֐�
        /// </summary>
        /// <param name="isMove"></param>
        /// <param name="isInteract"></param>
        public void Act_Move(bool isMove, bool isInteract, Vector2 vec)
        {
            //���ɂ��邩�ǂ����𔻒肷��
            if (groundChecker.LandingCheck() == false)
            {
                //Move���ɗ������Ă���Ƃ������Ƃ͂ӂ���𖳎����Ă���@���@ThroughFall
                animator.SetBool("Flg_Walk", false);
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
            else
            {
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
                
                //�o���q�̌��m
                if (ladderChecker.LadderClimbCheck())
                {
                    if (isInteract)
                    {
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                        stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                    }
                }

                //�����q�̌��m
                if (ladderChecker.LadderDownCheck())
                {
                    if (isInteract)
                    {
                        stateChangeEvent(PlayerStateEnum.LadderDown);
                    }
                }

                //�ڂ̑O���R������
                if (groundChecker.CheckGround(moveForward) == false)
                {
                    if(ladderChecker.LadderClimbCheck() || ladderChecker.LadderDownCheck())
                    {
                        rigidbody.velocity = Vector3.zero;
                    }
                    //�����̏���Ă��鏰�łӂ���邩�ǂ����̔���
                    else if (groundChecker.DizzyGroundFlg() == false)
                    {
                        //�ӂ���X�e�[�g�ɕύX
                        animator.SetBool("Flg_Walk", false);
                        rigidbody.velocity = Vector3.zero;
                        //colliCheck.ColiCheck();
                        stateChangeEvent(PlayerStateEnum.Dizzy);
                    }
                }
            }
        }
    }
}
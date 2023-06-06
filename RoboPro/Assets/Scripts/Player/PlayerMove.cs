using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;
using Zenject;

namespace Player
{
    public class PlayerMove : MonoBehaviour, IStateChange
    {
        [Inject]
        private ICameraVectorGetter cameraVectorGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;
        private IStateGetter stateGetter;
        private Goal goal;
        private GroundColliCheck colliCheck;

        private Vector3 moveForward;
        private GameObject moveEffect;

        // Start is called before the first frame update
        void Start()
        {
            colliCheck = GetComponent<GroundColliCheck>();
            stateGetter = GetComponent<IStateGetter>();

            moveEffect = transform.GetChild(1).gameObject;

            goal = GameObject.FindObjectOfType<Goal>();
            goal.OnStartInteract += Act_GoalPoint;
        }

        /// <summary>
        /// �ړ��֐�
        /// </summary>
        /// <param name="isMove"></param>
        /// <param name="isInteract"></param>
        public void Act_Move(bool isMove, bool isInteract, Vector2 vec)
        {
            stateGetter.GroundCheckGetter().CheckWall();
            //���ɂ��邩�ǂ����𔻒肷��
            if (stateGetter.GroundCheckGetter().LandingCheck())
            {
                //�J�����̊p�x����ړ��x�N�g����␳
                moveForward = cameraVectorGetter.VectorYGetter() * vec.y + cameraVectorGetter.VectorXGetter() * vec.x;
                moveForward = moveForward.normalized;

                if (isMove)
                {
                    //�ړ�����
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", true);
                    moveEffect.SetActive(true);
                    transform.LookAt(transform.position + moveForward);
                    stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * stateGetter.SpeedGetter(), stateGetter.RigidbodyGetter().velocity.y, transform.forward.z * stateGetter.SpeedGetter());
                }
                else
                {
                    //Stay��Ԃɂ���
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Stay);
                }
                AccesPoint_Check(isInteract);
                Dizzy_Check();
            }
            else
            {
                //Move���ɗ������Ă���Ƃ������Ƃ͂ӂ���𖳎����Ă���@���@ThroughFall
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
        }

        /// <summary>
        /// �A�N�Z�X�|�C���g�̉��Ԃ��߂��ɂ��邩
        /// </summary>
        private void AccesPoint_Check(bool isInteract)
        {
            int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
            if (index >= 0)
            {
                if (isInteract)
                {
                    bool access = stateGetter.GimmickAccessGetter().Access(index);
                    if (access)
                    {
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                        stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                        //�A�N�Z�X�|�C���g�ɐڑ�����
                        Vector3 pos = stateGetter.GimmickAccessGetter().GetPosition(index);
                        pos.y = this.transform.position.y;
                        transform.LookAt(pos);

                        stateChangeEvent(PlayerStateEnum.Access);
                    }
                }
            }
        }

        /// <summary>
        ///         //�ڂ̑O���R������
        /// </summary>
        private void Dizzy_Check()
        {
            if (stateGetter.GroundCheckGetter().CheckGround() == false)
            {
                //�����̏���Ă��鏰�łӂ���邩�ǂ����̔���
                if (stateGetter.GroundCheckGetter().DizzyGroundFlg())
                {
                    //�ӂ���X�e�[�g�ɕύX
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Dizzy);
                }
            }
            else
            {
                if (SideCheck() == false)
                {
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                }
            }
        }

        private bool SideCheck()
        {
            if (stateGetter.GroundCheckGetter().CheckSideGround_Left() && stateGetter.GroundCheckGetter().CheckSideGround_Right())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Act_GoalPoint()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("PlayerMove", false);
            Vector3 pos = goal.gameObject.transform.position;
            pos.y = this.transform.position.y;
            transform.LookAt(pos);
            stateChangeEvent(PlayerStateEnum.Access);
        }
    }
}
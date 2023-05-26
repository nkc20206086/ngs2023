using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;
using Zenject;
using InteractUI;

namespace Player
{
    public class PlayerMove : MonoBehaviour, IStateChange
    {
        [Inject]
        private IInteractUIControllable interactUIControllable;

        private GroundColliCheck colliCheck;
        private IStateGetter stateGetter;
        private ICameraVectorGetter cameraVectorGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector3 moveForward;

        Vector3 defaultScale;

        // Start is called before the first frame update
        void Start()
        {
            defaultScale = transform.lossyScale;
            cameraVectorGetter = Locator<ICameraVectorGetter>.GetT();
            colliCheck = GetComponent<GroundColliCheck>();
            stateGetter = GetComponent<IStateGetter>();
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
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                //Move���ɗ������Ă���Ƃ������Ƃ͂ӂ���𖳎����Ă���@���@ThroughFall
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
            else
            {
                moveForward = cameraVectorGetter.VectorYGetter() * vec.y + cameraVectorGetter.VectorXGetter() * vec.x;
                moveForward = moveForward.normalized;

                if (isMove)
                {
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", true);
                    transform.LookAt(transform.position + moveForward);
                    stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * stateGetter.SpeedGetter(), stateGetter.RigidbodyGetter().velocity.y, transform.forward.z * stateGetter.SpeedGetter());
                }
                else
                {
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Stay);
                }
                
                //�o���q�̌��m
                if (stateGetter.LadderCheckGetter().LadderClimbCheck())
                {
                    if (isInteract)
                    {
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                        stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                    }
                }

                //�����q�̌��m
                if (stateGetter.LadderCheckGetter().LadderDownCheck())
                {
                    if (isInteract)
                    {
                        stateChangeEvent(PlayerStateEnum.LadderDown);
                    }
                }

                //�A�N�Z�X�|�C���g�̉��Ԃ��߂��ɂ��邩
                //int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
                //if (index >= 0)
                //{
                //    //UI�\��
                //    Vector3 pos = stateGetter.GimmickAccessGetter().Access(index);
                //    interactUIControllable.SetPosition(pos);
                //    interactUIControllable.ShowUI(ControllerType.Keyboard, InteractKinds.ReturnKey);
                //    if (isInteract)
                //    {
                //        //�A�N�Z�X�|�C���g�ɐڑ�����
                //        pos.y = this.transform.position.y;
                //        transform.LookAt(pos);

                //        stateChangeEvent(PlayerStateEnum.Access);
                //    }
                //}
                //else
                //{
                //    interactUIControllable.HideUI();
                //}
                
                //�ڂ̑O���R������
                if (stateGetter.GroundCheckGetter().CheckGround(moveForward) == false)
                {
                    if(stateGetter.LadderCheckGetter().LadderClimbCheck() || stateGetter.LadderCheckGetter().LadderDownCheck())
                    {
                        stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    }
                    //�����̏���Ă��鏰�łӂ���邩�ǂ����̔���
                    else if (stateGetter.GroundCheckGetter().DizzyGroundFlg() == false)
                    {
                        //�ӂ���X�e�[�g�ɕύX
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Walk", false);
                        stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                        //colliCheck.ColiCheck();
                        stateChangeEvent(PlayerStateEnum.Dizzy);
                    }
                }
            }
        }
    }
}
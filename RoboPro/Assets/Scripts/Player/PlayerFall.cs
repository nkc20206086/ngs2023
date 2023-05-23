using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFall : MonoBehaviour, IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        private Vector2 jumpVec;
        private float startFallYVector;
        private bool isThroughFall;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();

            jumpVec = stateGetter.JumpPowerGetter();
        }

        /// <summary>
        /// �����֐�
        /// </summary>
        public void Act_Fall()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);

            //��э~��邽�߂̏��W�����v
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, transform.up.y * jumpVec.y, transform.forward.z * jumpVec.x);

            //false��������󒆂ɂ���
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                //�󒆗������X�e�[�g�ɕύX
                stateChangeEvent(PlayerStateEnum.Falling);
            }
        }

        /// <summary>
        /// �ӂ���𖳎����������֐�
        /// </summary>
        public void Act_ThroughFall()
        {
            startFallYVector = transform.position.y;
            isThroughFall = true;
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            stateGetter.RigidbodyGetter().velocity = new Vector3(transform.forward.x * jumpVec.x, stateGetter.RigidbodyGetter().velocity.y, transform.forward.z * jumpVec.x);
            stateChangeEvent(PlayerStateEnum.Falling);
        }

        /// <summary>
        /// ������
        /// </summary>
        public void Act_Falling()
        {
            //true�ɂȂ�����n�ʂɒ��n���Ă���
            if (stateGetter.GroundCheckGetter().LandingCheck())
            {
                if(isThroughFall)
                {
                    float fallingYVector = startFallYVector - transform.position.y;
                    isThroughFall = false;
                    if (stateGetter.DeathHeigthGetter() < fallingYVector)
                    {
                        stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", false);
                        stateChangeEvent(PlayerStateEnum.Die);
                    }
                    else
                    {
                        stateChangeEvent(PlayerStateEnum.Landing);
                    }
                }
                else
                { 
                    stateChangeEvent(PlayerStateEnum.Landing);
                }

            }
        }
    }
}
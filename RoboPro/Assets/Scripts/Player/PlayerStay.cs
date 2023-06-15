using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStay : MonoBehaviour,IStateChange
    {
        private Goal goal;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            goal = GameObject.FindObjectOfType<Goal>();
            goal.OnStartInteract += Act_GoalPoint;

            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Stay(bool isMove, bool isInteract)
        {
            if (isMove)
            {
                stateChangeEvent(PlayerStateEnum.Move);
            }


            Ladder_Check(isInteract);
            AccessPoint_Check(isInteract);

            Dizzy_Check(isInteract);

            //���ɂ��邩�ǂ����𔻒肷��
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
            }
        }

        /// <summary>
        /// ��q���߂��ɂ��邩�ǂ���
        /// </summary>
        /// <param name="isInteract"></param>
        private void Ladder_Check(bool isInteract)
        {
            if (stateGetter.LadderCheckGetter().LadderClimbCheck())
            {
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                }
            }

            if (stateGetter.LadderCheckGetter().LadderDownCheck())
            {
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderDown);
                }
            }
        }

        /// <summary>
        /// �A�N�Z�X�|�C���g�̉��Ԃ��߂��ɂ��邩
        /// </summary>
        /// <param name="isInteract"></param>
        private void AccessPoint_Check(bool isInteract)
        {
            int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
            if (index >= 0)
            {
                if (isInteract)
                {
                    bool access = stateGetter.GimmickAccessGetter().Access(index);
                    if (access)
                    {
                        //�A�N�Z�X�|�C���g�ɐڑ�����
                        Vector3 pos = stateGetter.GimmickAccessGetter().GetPosition(index);
                        pos.y = this.transform.position.y;
                        transform.LookAt(pos);

                        stateChangeEvent(PlayerStateEnum.Access);
                    }
                }
            }
        }

        private void Act_GoalPoint()
        {
            Vector3 pos = goal.gameObject.transform.position;
            pos.y = this.transform.position.y;
            transform.LookAt(pos);
            stateChangeEvent(PlayerStateEnum.Access);
        }

        private void Dizzy_Check(bool isInteract)
        {
            if (isInteract == false) return;
            if (stateGetter.GroundCheckGetter().CheckGround() == false)
            {
                //�����̏���Ă��鏰�łӂ���邩�ǂ����̔���
                if (stateGetter.GroundCheckGetter().DizzyGroundFlg())
                {
                    //�ӂ���X�e�[�g�ɕύX
                    stateGetter.RigidbodyGetter().velocity = Vector3.zero;
                    stateChangeEvent(PlayerStateEnum.Dizzy);
                }
            }
        }
    }
}
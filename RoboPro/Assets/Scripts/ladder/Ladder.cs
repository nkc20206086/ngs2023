using System.Collections.Generic;
using UnityEngine;

namespace Ladder
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField, Header("���邩�𔻒肷��㑤�̃��C�̒���")]
        private float checkUpRayLength;
        [SerializeField, Header("���邩�𔻒肷�鉡���̃��C�̒���")]
        private float checkSideRayLength;
        [SerializeField, Header("�R���{�̓o��鍂��")]
        private float climbableHigh;
        [SerializeField, Header("�R���{�̓o���O���̒���")]
        private float climableForwardLegth;
        [SerializeField, Header("�R���{����э~��邱�Ƃ��ł��鍂��")]
        private float dropableHigh;
        [SerializeField]
        private LayerMask checkGroundMask;
        [SerializeField]
        private bool debugMode;
        public bool isTopLadder;
        private BoxCollider boxCollider;
        private Vector3 defaultScale = Vector3.zero;
        void Start()
        {
            defaultScale = transform.lossyScale;
            boxCollider = GetComponent<BoxCollider>();
        }
        private void Update()
        {
            Vector3 lossScale = transform.lossyScale;
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(
                    localScale.x / lossScale.x * defaultScale.x,
                    localScale.y / lossScale.y * defaultScale.y,
                    localScale.z / lossScale.z * defaultScale.z);
        }
        public bool UpGroundCheck()
        {
            bool isHit = false;
            //if (isTopLadder)
            //{
            //    isHit = TopCheck();
            //}
            //SideCheck();

            return isHit || SideCheck() || ClimbableCheck();
        }
        private bool SideCheck()
        {
            Vector3 leftUpPos = new Vector3(
                 transform.position.x - transform.lossyScale.x * 0.5f * boxCollider.size.x,
                 transform.position.y,
                 transform.position.z - (transform.lossyScale.z * 0.5f * boxCollider.size.z) - checkSideRayLength);
            Vector3 rightUpPos = new Vector3(
                transform.position.x + transform.lossyScale.x * 0.5f * boxCollider.size.x,
                transform.position.y,
                transform.position.z - (transform.lossyScale.z * 0.5f * boxCollider.size.z) - checkSideRayLength);
            Vector3 leftDownPos = new Vector3(
               transform.position.x - transform.lossyScale.x * 0.5f * boxCollider.size.x,
               transform.position.y,
               transform.position.z - transform.lossyScale.z * boxCollider.size.z);
            Vector3 rightDownPos = new Vector3(
               transform.position.x + transform.lossyScale.x * 0.5f * boxCollider.size.x,
               transform.position.y,
               transform.position.z - transform.lossyScale.z * boxCollider.size.z);
            Vector3 centorPos = new Vector3(
                transform.position.x,
               transform.position.y,
               transform.position.z - transform.lossyScale.z * boxCollider.size.z - checkSideRayLength * 0.5f);
            Vector3 rightCentorPos = new Vector3(
               transform.position.x + transform.lossyScale.x * 0.5f * boxCollider.size.x,
              transform.position.y,
              transform.position.z - transform.lossyScale.z * boxCollider.size.z - checkSideRayLength * 0.5f);
            Vector3 leftCentorPos = new Vector3(
               transform.position.x - transform.lossyScale.x * 0.5f * boxCollider.size.x,
              transform.position.y,
              transform.position.z - transform.lossyScale.z * boxCollider.size.z - checkSideRayLength * 0.5f);
            float rayLength = isTopLadder ? transform.lossyScale.z * boxCollider.size.z + checkUpRayLength : transform.lossyScale.z * boxCollider.size.z;
            if (debugMode)
            {
                Debug.DrawRay(leftUpPos, transform.up * rayLength, Color.red);
                Debug.DrawRay(rightUpPos, transform.up * rayLength, Color.red);
                Debug.DrawRay(leftDownPos, transform.up * rayLength, Color.red);
                Debug.DrawRay(rightDownPos, transform.up * rayLength, Color.red);
                Debug.DrawRay(centorPos, transform.up * rayLength, Color.red);
                Debug.DrawRay(rightCentorPos, transform.up * rayLength, Color.red);
                Debug.DrawRay(leftCentorPos, transform.up * rayLength, Color.red);
            }
            bool leftUpHit = Physics.Raycast(leftUpPos, transform.up, rayLength, checkGroundMask);
            bool rightUpHit = Physics.Raycast(rightUpPos, transform.up, rayLength, checkGroundMask);
            bool leftDownHit = Physics.Raycast(leftDownPos, transform.up, rayLength, checkGroundMask);
            bool rightDownHit = Physics.Raycast(rightDownPos, transform.up, rayLength, checkGroundMask);
            bool leftCentornHit = Physics.Raycast(leftCentorPos, transform.up, rayLength, checkGroundMask);
            bool rightCentorHit = Physics.Raycast(rightCentorPos, transform.up, rayLength, checkGroundMask);
            bool centorhit = Physics.Raycast(centorPos, transform.up, rayLength, checkGroundMask);

            //�ǂ̃��C�ɂ��Ԃ���Ȃ������ꍇ�o���
            return !(leftUpHit || rightUpHit || leftDownHit || rightDownHit || leftCentornHit || rightCentorHit || centorhit);
        }

        private bool ClimbableCheck()
        {
            //�������ɏ�Q�����Ȃ����m�F
            //���C���o���ʒu���쐬
            Vector3 rayStartPos = new Vector3(
                transform.position.x,
                transform.position.y + transform.lossyScale.y * boxCollider.size.y + climbableHigh,
                transform.position.z);
            if(debugMode) Debug.DrawRay(rayStartPos, -transform.forward * climableForwardLegth, Color.green);
            bool forwordCheck = Physics.Raycast(rayStartPos, -transform.forward, climableForwardLegth, checkGroundMask);
            //�������Ă�����o��Ȃ�
            if (forwordCheck) return false;

            //�������ɏ������邩
            Vector3 downCheckStartPos = new Vector3(
                transform.position.x,
                transform.position.y + transform.lossyScale.y * boxCollider.size.y + 0.1f,
                transform.position.z + transform.lossyScale.z * boxCollider.size.z + 0.2f);
            if (debugMode) Debug.DrawRay(downCheckStartPos, -transform.up * dropableHigh, Color.black);
            bool dropableCheck = Physics.Raycast(downCheckStartPos, -transform.up, dropableHigh, checkGroundMask);
            return !dropableCheck;
        }
        /// <summary>
        /// �v���C���[�Ƃ͂����̊Ԃɏ�Q�����Ȃ����m�F
        /// </summary>
        /// <returns></returns>
        public bool PlayerOnLadderCheck(Vector3 playerPos, Vector3 ladderPos)
        {

            bool result = Physics.Linecast(playerPos, ladderPos, checkGroundMask);
            if (debugMode) Debug.DrawLine(playerPos, ladderPos, Color.red);
            return !result;
        }

        #region �g��Ȃ�����
        private bool TopCheck()
        {
            //���C���o���n�_���쐬
            Vector3 leftUpPos = new Vector3(
                transform.position.x - transform.lossyScale.x * 0.5f * boxCollider.size.x,
                transform.position.y + transform.lossyScale.y * boxCollider.size.y,
                transform.position.z - (transform.lossyScale.z * 0.5f * boxCollider.size.z));

            Vector3 rightUpPos = new Vector3(
               transform.position.x + transform.lossyScale.x * 0.5f * boxCollider.size.x,
               transform.position.y + transform.lossyScale.y * boxCollider.size.y,
               transform.position.z - (transform.lossyScale.z * 0.5f * boxCollider.size.z));

            Vector3 leftDownPos = new Vector3(
                transform.position.x + transform.lossyScale.x * 0.5f * boxCollider.size.x,
                transform.position.y + transform.lossyScale.y * boxCollider.size.y,
                transform.position.z + (transform.lossyScale.z * 0.5f * boxCollider.size.z));

            Vector3 rightDownPos = new Vector3(
                transform.position.x - transform.lossyScale.x * 0.5f * boxCollider.size.x,
                transform.position.y + transform.lossyScale.y * boxCollider.size.y,
                transform.position.z + (transform.lossyScale.z * 0.5f * boxCollider.size.z));

            //���C���o���Ă����蔻����s��
            bool leftUpHit = Physics.Raycast(leftUpPos, transform.up, checkUpRayLength, checkGroundMask);
            bool rightUpHit = Physics.Raycast(rightUpPos, transform.up, checkUpRayLength, checkGroundMask);
            bool leftDownHit = Physics.Raycast(leftDownPos, transform.up, checkUpRayLength, checkGroundMask);
            bool rightDownHit = Physics.Raycast(rightDownPos, transform.up, checkUpRayLength, checkGroundMask);
            if (debugMode)
            {
                Debug.DrawRay(leftUpPos, transform.up, Color.red, checkUpRayLength);
                Debug.DrawRay(rightUpPos, transform.up, Color.red, checkUpRayLength);
                Debug.DrawRay(leftDownPos, transform.up, Color.red, checkUpRayLength);
                Debug.DrawRay(rightDownPos, transform.up, Color.red, checkUpRayLength);
            }
            return (leftUpHit || rightUpHit || leftDownHit || rightDownHit);
        }
        #endregion
    }
}

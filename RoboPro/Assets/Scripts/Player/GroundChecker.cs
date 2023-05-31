using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GroundChecker : MonoBehaviour
    {
        private float dir = 1f;
        private Vector3 originVector;
        private Vector3 rayPosition;
        private bool floorFlg;
        private bool downFloorFlg;
        private bool dizzyGroundFlg;
        private string parentGroundName;
        [SerializeField]
        private float rayLength;
        [SerializeField]
        private float landingLength;
        [SerializeField]
        private float liveRayLength;
        [SerializeField]
        private int splitHeightVectorNum;

        [SerializeField]
        private LayerMask GroundMask;
        [SerializeField]
        private LayerMask parentMask;
        private CapsuleCollider capsuleCollider;

        private float[] splitHeightVectorArray;

        private bool isCheckWall;
        private bool parentResetFlg;
        private string parentOldName;

        private void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            //���������̔z���p��(Y = 0����Ȃ̂�+1)
            splitHeightVectorArray = new float[splitHeightVectorNum + 1];
            //1�����̍������v�Z
            float splitVec = capsuleCollider.height / splitHeightVectorNum;

            for (int i = 0; i < splitHeightVectorNum + 1; i++)
            {
                splitHeightVectorArray[i] = splitVec * i;
            }
        }

        /// <summary>
        /// ���n���Ă��邩����
        /// </summary>
        /// <returns></returns>
        public bool LandingCheck()
        {
            bool floatingFlg;
            Vector3 playerPosition = transform.position + new Vector3(0f, 0.1f, 0f);
            RaycastHit ray = new RaycastHit();
            floatingFlg = Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, GroundMask);
            //Debug.DrawRay(playerPosition, Vector3.down * landingLength);
            return floatingFlg;
        }

        /// <summary>
        /// �ڂ̑O���R������
        /// </summary>
        /// <param name="playerDir"></param>
        /// <returns></returns>
        public bool CheckGround()
        {
            //��������p���C�̌��_�̌v�Z
            Vector3 playerForwardVec = gameObject.transform.position;
            playerForwardVec.x += transform.forward.x * 0.2f;
            playerForwardVec.z += transform.forward.z * 0.2f;
            playerForwardVec.y += 0.2f;

            //���_����90�x�������Ƀ��C���o��
            floorFlg = Physics.Raycast(playerForwardVec, Vector3.down, rayLength);
            Debug.DrawRay(playerForwardVec, Vector3.down * rayLength);

            return floorFlg;
        }

        public bool CheckSideGround()
        {
            Vector3 playerForwardVec = gameObject.transform.position;
            playerForwardVec.x += transform.forward.x * 0.5f;
            playerForwardVec.z += transform.forward.z * 0.5f;
            playerForwardVec.y += 0.2f;

            Vector3 normalizedVec = playerForwardVec.normalized;

            Vector3 rotateVector = Quaternion.Euler(0f, 0f, 90f) * normalizedVec;

            Vector3 pointVectorX = rotateVector + playerForwardVec;
            Vector3 pointVectorZ = rotateVector - playerForwardVec;

            //Vector3 mixedVecRight = new Vector3(playerForwardVec.x * 0.2f, playerForwardVec.y, playerForwardVec.z * 0.2f);
            //Vector3 mixedVecLeft = new Vector3(playerForwardVec.x * -0.2f, playerForwardVec.y, playerForwardVec.z * -0.2f);

            Debug.DrawRay(playerForwardVec, Vector3.down * rayLength);
            Debug.DrawRay(pointVectorX, Vector3.down * rayLength);
            Debug.DrawRay(pointVectorZ, Vector3.down * rayLength);


            return true;
        }

        /// <summary>
        /// �ڂ̑O�ɕǂ�����̂��𔻒�
        /// </summary>
        /// <returns></returns>
        public bool CheckWall()
        {
            //Ray�̔��ˈʒu�̐ݒ�
            Vector3 splitVec = gameObject.transform.position;
            //splitVec.x += gameObject.transform.forward.x * 0.1f;
            //splitVec.z += gameObject.transform.forward.z * 0.1f;

            RaycastHit ray = new RaycastHit();
            for (int i = 0; i < splitHeightVectorArray.Length; i++)
            {
                splitVec.y = gameObject.transform.position.y;
                //Y�l���v�Z�����l�ɐݒ�
                splitVec.y += splitHeightVectorArray[i] + 0.1f;
                //�R���C�_�[�̒��a��Ray�𔭎�
                Physics.Raycast(splitVec, transform.forward, out ray, capsuleCollider.radius * 3f);
                //Debug.DrawRay(splitVec, transform.forward * capsuleCollider.radius * 3f);
                //1�{�ł�Ray����������������ǂ��葦break
                if (ray.collider == null)
                {
                    isCheckWall = false;
                }
                else
                {
                    isCheckWall = true;
                    break;
                }
            }
            return isCheckWall;
        }

        /// <summary>
        /// ���S���鍂��������
        /// </summary>
        /// <returns></returns>
        public bool CheckDeathHeight()
        {
            RaycastHit ray = new RaycastHit();

            rayPosition = gameObject.transform.position;
            rayPosition.x += transform.forward.x * 0.5f;
            rayPosition.z += transform.forward.z * 0.5f;

            Physics.Raycast(rayPosition, Vector3.down, out ray, liveRayLength, GroundMask);

            //Null�Ȃ玀�ʂ����э~���Ȃ�
            if (ray.collider == null) return true;
            return false;
        }

        /// <summary>
        /// ���̃��C���[�𔻒�
        /// </summary>
        /// <returns></returns>
        public bool DizzyGroundFlg()
        {
            Vector3 playerPosition = transform.position + new Vector3(0f, 0.1f, 0f);
            RaycastHit ray = new RaycastHit();
            Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, GroundMask);

            //���C���[�ԍ���9�ԂȂ�ӂ���Ȃ�
            if (ray.collider == null || ray.collider.gameObject.layer == 9) return false;

            return true;
        }

        /// <summary>
        /// �����e�q�֌W�ɂȂ�ׂ����Ȃ̂��𔻒肷��
        /// </summary>
        public void CheckParentGround()
        {
            Vector3 playerPosition = gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
            RaycastHit ray = new RaycastHit();
            Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, parentMask);

            //null���������ł��Ă��Ȃ�
            if (ray.collider == null)
            {
                if (string.IsNullOrWhiteSpace(parentOldName) == false)
                {
                    gameObject.transform.parent = null;
                    parentOldName = "";
                    parentResetFlg = true;
                }
            }
            //null�A�܂��͖��O�������Ȃ�return
            else
            {
                if (ray.collider == null || ray.collider.gameObject.name == parentOldName) return;
                parentOldName = ray.collider.gameObject.name;
                gameObject.transform.parent = ray.collider.gameObject.transform;
            }
        }
    }
}
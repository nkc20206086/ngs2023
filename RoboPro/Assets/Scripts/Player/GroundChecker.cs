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
            //分割数分の配列を用意(Y = 0からなので+1)
            splitHeightVectorArray = new float[splitHeightVectorNum + 1];
            //1分割の高さを計算
            float splitVec = capsuleCollider.height / splitHeightVectorNum;

            for (int i = 0; i < splitHeightVectorNum + 1; i++)
            {
                splitHeightVectorArray[i] = splitVec * i;
            }
        }

        /// <summary>
        /// 着地しているか判定
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
        /// 目の前が崖か判定
        /// </summary>
        /// <param name="playerDir"></param>
        /// <returns></returns>
        public bool CheckGround()
        {
            //落下判定用レイの原点の計算
            Vector3 playerForwardVec = gameObject.transform.position;
            playerForwardVec.x += transform.forward.x * 0.2f;
            playerForwardVec.z += transform.forward.z * 0.2f;
            playerForwardVec.y += 0.2f;

            //原点から90度下向きにレイを出す
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
        /// 目の前に壁があるのかを判定
        /// </summary>
        /// <returns></returns>
        public bool CheckWall()
        {
            //Rayの発射位置の設定
            Vector3 splitVec = gameObject.transform.position;
            //splitVec.x += gameObject.transform.forward.x * 0.1f;
            //splitVec.z += gameObject.transform.forward.z * 0.1f;

            RaycastHit ray = new RaycastHit();
            for (int i = 0; i < splitHeightVectorArray.Length; i++)
            {
                splitVec.y = gameObject.transform.position.y;
                //Y値を計算した値に設定
                splitVec.y += splitHeightVectorArray[i] + 0.1f;
                //コライダーの直径分Rayを発射
                Physics.Raycast(splitVec, transform.forward, out ray, capsuleCollider.radius * 3f);
                //Debug.DrawRay(splitVec, transform.forward * capsuleCollider.radius * 3f);
                //1本でもRayが引っかかったら壁あり即break
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
        /// 死亡する高さか判定
        /// </summary>
        /// <returns></returns>
        public bool CheckDeathHeight()
        {
            RaycastHit ray = new RaycastHit();

            rayPosition = gameObject.transform.position;
            rayPosition.x += transform.forward.x * 0.5f;
            rayPosition.z += transform.forward.z * 0.5f;

            Physics.Raycast(rayPosition, Vector3.down, out ray, liveRayLength, GroundMask);

            //Nullなら死ぬから飛び降りれない
            if (ray.collider == null) return true;
            return false;
        }

        /// <summary>
        /// 床のレイヤーを判定
        /// </summary>
        /// <returns></returns>
        public bool DizzyGroundFlg()
        {
            Vector3 playerPosition = transform.position + new Vector3(0f, 0.1f, 0f);
            RaycastHit ray = new RaycastHit();
            Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, GroundMask);

            //レイヤー番号が9番ならふらつけない
            if (ray.collider == null || ray.collider.gameObject.layer == 9) return false;

            return true;
        }

        /// <summary>
        /// 床が親子関係になるべき床なのかを判定する
        /// </summary>
        public void CheckParentGround()
        {
            Vector3 playerPosition = gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
            RaycastHit ray = new RaycastHit();
            Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, parentMask);

            //nullかつ初期化できていない
            if (ray.collider == null)
            {
                if (string.IsNullOrWhiteSpace(parentOldName) == false)
                {
                    gameObject.transform.parent = null;
                    parentOldName = "";
                    parentResetFlg = true;
                }
            }
            //null、または名前が同じならreturn
            else
            {
                if (ray.collider == null || ray.collider.gameObject.name == parentOldName) return;
                parentOldName = ray.collider.gameObject.name;
                gameObject.transform.parent = ray.collider.gameObject.transform;
            }
        }
    }
}
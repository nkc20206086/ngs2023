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
        private LayerMask layerMask;
        private CapsuleCollider capsuleCollider;

        private float[] splitHeightVectorArray;
        private bool isCheckWall;
        private Vector3 parentOldScale;

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
            floatingFlg = Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, layerMask);
            Debug.DrawRay(playerPosition, Vector3.down * landingLength);
            return floatingFlg;
        }

        /// <summary>
        /// 目の前が崖か判定
        /// </summary>
        /// <param name="playerDir"></param>
        /// <returns></returns>
        public bool CheckGround(Vector3 playerDir)
        {
            playerDir = playerDir / 4;
            //落下判定用レイの原点の計算
            originVector = transform.position + playerDir + new Vector3(0, 0.1f, 0f);
            //原点から90度下向きにレイを出す
            floorFlg = Physics.Raycast(originVector, -transform.up, rayLength);

            return floorFlg;
        }

        /// <summary>
        /// 目の前に壁があるのかを判定
        /// </summary>
        /// <returns></returns>
        public bool CheckWall()
        {
            //Rayの発射位置の設定
            Vector3 splitVec = transform.position;
            splitVec.x += transform.forward.x * 0.2f;
            splitVec.z += transform.forward.z * 0.2f;

            RaycastHit ray = new RaycastHit();
            for(int i = 0; i < splitHeightVectorArray.Length;i++)
            {
                //Y値を計算した値に設定
                splitVec.y = splitHeightVectorArray[i];
                //コライダーの直径分Rayを発射
                Physics.Raycast(splitVec, transform.forward,out ray,capsuleCollider.radius * 2);
                Debug.DrawRay(splitVec, transform.forward * capsuleCollider.radius * 2);
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
            bool deathFlg;
            
            RaycastHit ray = new RaycastHit();

            rayPosition = transform.position;
            rayPosition.x += transform.forward.x * 0.5f;
            rayPosition.z += transform.forward.z * 0.5f;
            
            Physics.Raycast(rayPosition, Vector3.down, out ray, liveRayLength,layerMask);

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
            Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, layerMask);

            //レイヤー番号が8番なら
            if (ray.collider == null || ray.collider.gameObject.layer != 8) return true;

            return false;
        }

        public void  CheckParentGround()
        {
            Vector3 playerPosition = transform.position + new Vector3(0f, 0.1f, 0f);
            RaycastHit ray = new RaycastHit();
            Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, layerMask);

            if (ray.collider == null || ray.collider.gameObject.transform.localScale == parentOldScale) return;
            parentOldScale = ray.collider.gameObject.transform.localScale;
            gameObject.transform.parent = ray.collider.gameObject.transform;
        }
    }
}
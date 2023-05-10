using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GroundColliCheck : MonoBehaviour
    {
        Vector3 hitNormalVec = new Vector3();
        GameObject groundObj;

        public void ColiCheck()
        {
            //プレイヤーは上を向いているに違いないため地面ベクトルからY軸を削除したベクトルを出す。
            Vector2 groundCalcVec = new Vector2(groundObj.transform.position.x, groundObj.transform.position.z);
            Vector2 playerCalcVec = new Vector2(transform.position.x, transform.position.z);
            //二次元ベクトルにしたためベクトルの差から角度を取得可能
            Vector2 vec = groundCalcVec - playerCalcVec;
            float angle = VectorToAngle(vec);
            //地面のZ軸の表で衝突していた場合
            if (hitNormalVec == groundObj.transform.forward)
            {
                //どの方向にプレイヤーが向くべきかを出す
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, groundObj.transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, groundObj.transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, groundObj.transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, groundObj.transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のZ軸の裏で衝突していた場合
            else if (hitNormalVec == -groundObj.transform.forward)
            {
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, groundObj.transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, groundObj.transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, groundObj.transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, groundObj.transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のY軸の表で衝突していた場合
            if (hitNormalVec == groundObj.transform.up)
            {
                Debug.Log("upが一緒だった");
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, groundObj.transform.up);

                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, groundObj.transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のY軸の裏で衝突していた場合
            else if (hitNormalVec == -groundObj.transform.up)
            {
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, groundObj.transform.up);

                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, groundObj.transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のX軸の表で衝突していた場合
            if (hitNormalVec == groundObj.transform.right)
            {
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, groundObj.transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, groundObj.transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のX軸の裏で衝突していた場合
            else if (hitNormalVec == -groundObj.transform.right)
            {
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, groundObj.transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, groundObj.transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, groundObj.transform.up);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.LogError("予定外の地面ベクトルを検知 vec="+ hitNormalVec);
                Debug.LogError("予定外の地面ベクトルを検知 playerright="+ groundObj.transform.right);
                Debug.LogError("予定外の地面ベクトルを検知 playerUp="+ groundObj.transform.up);
                Debug.LogError("予定外の地面ベクトルを検知 playerright="+ groundObj.transform.forward);
            }
        }
        private Directions AngleToDirection(float angle)
        {
            float angle360 = Mathf.Repeat(angle, 360);
            angle360 -= 45;
            int a = (int)angle360 / 90;
            return (Directions)a;
        }

        enum Directions
        {
            Down, Right, Up, Left
        }
        private float VectorToAngle(Vector2 vec)
        {
            return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        }
        private void OnCollisionEnter(Collision collision)
        {
            //接触した面の法線ベクトルを取得  レイでもできる
            hitNormalVec = collision.contacts[0].normal;
            groundObj = collision.gameObject;
        }

    }
}

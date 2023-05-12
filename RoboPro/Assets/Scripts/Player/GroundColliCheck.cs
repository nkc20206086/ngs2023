using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GroundColliCheck : MonoBehaviour
    {
        Vector3 hitNormalVec = new Vector3();
        GameObject groundObj;
        [SerializeField] float CorrectionValue;
        private void Update()
        {
            //if(Input.GetKeyDown(KeyCode.Return))
            if (groundObj != null)
            {
                ColiCheck();
            }
        }
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
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, transform.up);
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
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のY軸の表で衝突していた場合
            else if (/*hitNormalVec == groundObj.transform.up*/isVectorEpuals(hitNormalVec, groundObj.transform.up))
            {
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のY軸の裏で衝突していた場合
            else if (hitNormalVec == -groundObj.transform.up/*isVectorEpuals(hitNormalVec,-groundObj.transform.position)*/)
            {
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.right, transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.right, transform.up);
                        break;
                    default:
                        break;
                }
            }
            //地面のX軸の表で衝突していた場合
            else if (hitNormalVec == groundObj.transform.right)
            {
                switch (AngleToDirection(angle))
                {
                    case Directions.Down:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, transform.up);
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
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Right:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.up, transform.up);
                        break;
                    case Directions.Up:
                        transform.rotation = Quaternion.LookRotation(groundObj.transform.forward, transform.up);
                        break;
                    case Directions.Left:
                        transform.rotation = Quaternion.LookRotation(-groundObj.transform.up, transform.up);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.LogError("予定外の地面ベクトルを検知 vec=" + hitNormalVec);
                Debug.LogError("予定外の地面ベクトルを検知 playerright=" + groundObj.transform.right);
                Debug.LogError("予定外の地面ベクトルを検知 playerUp=" + groundObj.transform.up);
                Debug.LogError("予定外の地面ベクトルを検知 playerright=" + groundObj.transform.forward);
            }
        }
        private Directions AngleToDirection(float angle)
        {
            float angle360 = Mathf.Repeat(angle - 45, 360);
            int num = (int)Mathf.Abs((angle360 / 90));
            return (Directions)num;
        }
        private bool isVectorEpuals(Vector3 normal, Vector3 pos)
        {
            //bool a = Mathf.Approximately( Mathf.Floor(vector3.x,))&& Mathf.Floor(vector3.y), Mathf.Floor(vector3.z)
            bool b = Mathf.Approximately(Mathf.Floor(normal.x), Mathf.Floor(pos.x)) &&
                Mathf.Approximately(Mathf.Floor(normal.y), Mathf.Floor(pos.y)) &&
                Mathf.Approximately(Mathf.Floor(normal.z), Mathf.Floor(pos.z));
            return b;
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
            //hitNormalVec = new Vector3(Mathf.Floor(collision.contacts[0].normal.x), Mathf.Floor(collision.contacts[0].normal.y), Mathf.Floor(collision.contacts[0].normal.z));
            hitNormalVec = collision.contacts[0].normal;
            groundObj = collision.gameObject;
            
        }

    }
}

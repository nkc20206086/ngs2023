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
            //�v���C���[�͏�������Ă���ɈႢ�Ȃ����ߒn�ʃx�N�g������Y�����폜�����x�N�g�����o���B
            Vector2 groundCalcVec = new Vector2(groundObj.transform.position.x, groundObj.transform.position.z);
            Vector2 playerCalcVec = new Vector2(transform.position.x, transform.position.z);
            //�񎟌��x�N�g���ɂ������߃x�N�g���̍�����p�x���擾�\
            Vector2 vec = groundCalcVec - playerCalcVec;
            float angle = VectorToAngle(vec);
            //�n�ʂ�Z���̕\�ŏՓ˂��Ă����ꍇ
            if (hitNormalVec == groundObj.transform.forward)
            {
                //�ǂ̕����Ƀv���C���[�������ׂ������o��
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
            //�n�ʂ�Z���̗��ŏՓ˂��Ă����ꍇ
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
            //�n�ʂ�Y���̕\�ŏՓ˂��Ă����ꍇ
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
            //�n�ʂ�Y���̗��ŏՓ˂��Ă����ꍇ
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
            //�n�ʂ�X���̕\�ŏՓ˂��Ă����ꍇ
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
            //�n�ʂ�X���̗��ŏՓ˂��Ă����ꍇ
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
                Debug.LogError("�\��O�̒n�ʃx�N�g�������m vec=" + hitNormalVec);
                Debug.LogError("�\��O�̒n�ʃx�N�g�������m playerright=" + groundObj.transform.right);
                Debug.LogError("�\��O�̒n�ʃx�N�g�������m playerUp=" + groundObj.transform.up);
                Debug.LogError("�\��O�̒n�ʃx�N�g�������m playerright=" + groundObj.transform.forward);
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
            //�ڐG�����ʂ̖@���x�N�g�����擾  ���C�ł��ł���
            //hitNormalVec = new Vector3(Mathf.Floor(collision.contacts[0].normal.x), Mathf.Floor(collision.contacts[0].normal.y), Mathf.Floor(collision.contacts[0].normal.z));
            hitNormalVec = collision.contacts[0].normal;
            groundObj = collision.gameObject;
            
        }

    }
}

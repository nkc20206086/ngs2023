using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainCamera
{
    public class CameraVector : MonoBehaviour, ICameraVectorGetter
    {
        private CameraVectorEnumDatas cameraVectorEnumDatas;
        private Vector3[] cameraVectorYArray;
        private Vector3[] cameraVectorXArray;

        private readonly int settingAngle = 45;
        private float angleY = 0;
        private float angleX = 45;
        private float rad = 0;

        // Start is called before the first frame update
        void Start()
        {
            cameraVectorYArray = new Vector3[(int)CameraVectorEnumDatas.CameraVectorEnum.vector_360];
            cameraVectorXArray = new Vector3[(int)CameraVectorEnumDatas.CameraVectorEnum.vector_360];

            for (int i = 0; i < cameraVectorYArray.Length; i++)
            {
                rad = angleY * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                cameraVectorYArray[i] = direction;
                angleY += settingAngle;
            }

            for (int i = 0; i < cameraVectorXArray.Length; i++)
            {
                angleX += settingAngle;
                rad = angleX * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                cameraVectorXArray[i] = direction;
            }
        }

        public int CameraVectorGetter()
        {
            // �J�����̊p�x�̎擾
            var cameraRot = Camera.main.transform.localEulerAngles.y;

            if (cameraRot < (int)CameraVectorEnumDatas.CameraVectorEnum.vector_0)
            {
                cameraRot = 360 - Mathf.Abs(cameraRot);
            }

            // �J�����̊p�x��45�x�Ŋ���
            float divisionAngle = cameraRot / settingAngle;

            // �������l���l�̌ܓ����ă|�C���g���o��
            int cameraPoint = (int)Math.Round(divisionAngle, MidpointRounding.AwayFromZero);

            // 360�x = 0�x
            if (cameraPoint == (int)CameraVectorEnumDatas.CameraVectorEnum.vector_360)
            {
                cameraPoint = (int)CameraVectorEnumDatas.CameraVectorEnum.vector_0;
            }
            return cameraPoint;
        }

        Vector3 ICameraVectorGetter.VectorYGetter()
        {
            return cameraVectorYArray[CameraVectorGetter()];
        }

        Vector3 ICameraVectorGetter.VectorXGetter()
        {
            return cameraVectorXArray[CameraVectorGetter()];
        }
    }
}
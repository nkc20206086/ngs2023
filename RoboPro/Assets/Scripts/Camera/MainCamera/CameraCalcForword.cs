using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainCamera
{
    public class CameraCalcForword : MonoBehaviour, ICameraVectorGetter
    {
        private CameraVectorEnumDatas cameraVectorEnumDatas;
        private Vector3[] cameraVectorYArray;
        private Vector3[] cameraVectorXArray;

        private readonly int settingAngle = 90;
        private float angleY = 0;
        private float angleX = 90;
        private float rad = 0;

        private void Awake()
        {
            //Locator<ICameraVectorGetter>.Bind(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            cameraVectorYArray = new Vector3[(int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_360];
            cameraVectorXArray = new Vector3[(int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_360];

            //�J������Y�p�x�␳�l���v�Z
            for (int i = 0; i < cameraVectorYArray.Length; i++)
            {
                //0�x����45�x���v�Z
                rad = angleY * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                cameraVectorYArray[i] = direction;
                angleY += settingAngle;
            }

            //�J������X�p�x�␳�l���v�Z
            for (int i = 0; i < cameraVectorXArray.Length; i++)
            {
                //45�x����45�x���v�Z
                rad = angleX * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                cameraVectorXArray[i] = direction;
                angleX += settingAngle;
            }
        }

        /// <summary>
        /// �J�������ǂ̃|�C���g�������Ă��邩�̌v�Z
        /// </summary>
        /// <returns></returns>
        public int CameraVectorGetter()
        {
            // �J�����̊p�x�̎擾
            var cameraRot = Camera.main.transform.localEulerAngles.y;

            if (cameraRot < (int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_0)
            {
                cameraRot = 360 - Mathf.Abs(cameraRot);
            }

            // �J�����̊p�x��45�x�Ŋ���
            float divisionAngle = cameraRot / settingAngle;

            // �������l���l�̌ܓ����ă|�C���g���o��
            int cameraPoint = (int)Math.Round(divisionAngle, MidpointRounding.AwayFromZero);

            // 360�x = 0�x
            if (cameraPoint == (int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_360)
            {
                cameraPoint = (int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_0;
            }
            return cameraPoint;
        }

        /// <summary>
        /// �J����Y�␳�l��n���֐�
        /// </summary>
        /// <returns></returns>
        Vector3 ICameraVectorGetter.VectorYGetter()
        {
            return cameraVectorYArray[CameraVectorGetter()];
        }

        /// <summary>
        /// �J����X�␳�l��n���֐�
        /// </summary>
        /// <returns></returns>
        Vector3 ICameraVectorGetter.VectorXGetter()
        {
            return cameraVectorXArray[CameraVectorGetter()];
        }
    }
}
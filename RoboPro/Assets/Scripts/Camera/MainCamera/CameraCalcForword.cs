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

            //カメラのY角度補正値を計算
            for (int i = 0; i < cameraVectorYArray.Length; i++)
            {
                //0度から45度ずつ計算
                rad = angleY * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                cameraVectorYArray[i] = direction;
                angleY += settingAngle;
            }

            //カメラのX角度補正値を計算
            for (int i = 0; i < cameraVectorXArray.Length; i++)
            {
                //45度から45度ずつ計算
                rad = angleX * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
                cameraVectorXArray[i] = direction;
                angleX += settingAngle;
            }
        }

        /// <summary>
        /// カメラがどのポイントを向いているかの計算
        /// </summary>
        /// <returns></returns>
        public int CameraVectorGetter()
        {
            // カメラの角度の取得
            var cameraRot = Camera.main.transform.localEulerAngles.y;

            if (cameraRot < (int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_0)
            {
                cameraRot = 360 - Mathf.Abs(cameraRot);
            }

            // カメラの角度を45度で割る
            float divisionAngle = cameraRot / settingAngle;

            // 割った値を四捨五入してポイントを出す
            int cameraPoint = (int)Math.Round(divisionAngle, MidpointRounding.AwayFromZero);

            // 360度 = 0度
            if (cameraPoint == (int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_360)
            {
                cameraPoint = (int)CameraVectorEnumDatas.CameraVectorEnum_90.vector_0;
            }
            return cameraPoint;
        }

        /// <summary>
        /// カメラY補正値を渡す関数
        /// </summary>
        /// <returns></returns>
        Vector3 ICameraVectorGetter.VectorYGetter()
        {
            return cameraVectorYArray[CameraVectorGetter()];
        }

        /// <summary>
        /// カメラX補正値を渡す関数
        /// </summary>
        /// <returns></returns>
        Vector3 ICameraVectorGetter.VectorXGetter()
        {
            return cameraVectorXArray[CameraVectorGetter()];
        }
    }
}
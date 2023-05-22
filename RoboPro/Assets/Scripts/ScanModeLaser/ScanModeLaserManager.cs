using System.Collections.Generic;
using UnityEngine;

namespace ScanMode
{
    public class ScanModeLaserManager : MonoBehaviour, IScanModeLaserManageable
    {
        [SerializeField]
        private GameObject laserObjPrefab;

        [SerializeField, Tooltip("レーザーの太さ")]
        private float laserWidth = 0.1f;

        private List<GameObject> laserObjList = new List<GameObject>();

        private MaterialPropertyBlock mpb;
        private static string colorProp = "_Color";

        void IScanModeLaserManageable.LaserInit(List<ScanModeLaserTargetInfo> laserInfoList)
        {
            for (int i = 0; i < laserInfoList.Count; i++)
            {
                GameObject laser = Instantiate(laserObjPrefab, this.gameObject.transform);
                laser.transform.localScale = new Vector3(laserWidth, laserWidth, laserWidth);
                laserObjList.Add(laser);
            }

            SetLaserColor(laserInfoList);
            SetLaserPos(laserInfoList);
            HideLaser();
        }

        public void SetLaserPos(List<ScanModeLaserTargetInfo> laserInfoList)
        {
            for (int i = 0; i < laserInfoList.Count; i++)
            {
                Vector3 dir = laserInfoList[i].t1.position - laserInfoList[i].t0.position;
                dir = Vector3.Normalize(dir);
                laserObjList[i].transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
                float length = Vector3.Distance(laserInfoList[i].t0.position, laserInfoList[i].t1.position);
                laserObjList[i].transform.localScale = new Vector3(length, laserWidth, laserWidth);
                Vector3 center = (laserInfoList[i].t0.position + laserInfoList[i].t1.position) * 0.5f;
                laserObjList[i].transform.position = center;
            }
        }

        /// <summary>
        /// レーザーの色を設定する
        /// </summary>
        /// <param name="laserInfoList">レーザー情報のList</param>
        private void SetLaserColor(List<ScanModeLaserTargetInfo> laserInfoList)
        {
            mpb = new MaterialPropertyBlock();
            for (int i = 0; i < laserObjList.Count; i++)
            {
                MeshRenderer[] renderers = laserObjList[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < renderers.Length; j++)
                {
                    mpb.SetColor(colorProp, laserInfoList[i].color);
                    renderers[j].SetPropertyBlock(mpb);
                }
            }
        }

        void IScanModeLaserManageable.ShowLaser()
        {
            for (int i = 0; i < laserObjList.Count; i++)
            {
                laserObjList[i].SetActive(true);
            }
        }

        public void HideLaser()
        {
            for (int i = 0; i < laserObjList.Count; i++)
            {
                laserObjList[i].SetActive(false);
            }
        }

        void IScanModeLaserManageable.ClearLaserData()
        {
            laserObjList.Clear();
        }
    }
}

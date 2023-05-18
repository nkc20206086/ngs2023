using System.Collections.Generic;
using UnityEngine;

namespace ScanMode
{
    public class ScanModeLaserManager : MonoBehaviour, IScanModeLaserManageable
    {
        [SerializeField]
        private GameObject laserObjPrefab;

        [SerializeField, Tooltip("ÉåÅ[ÉUÅ[ÇÃëæÇ≥")]
        private float laserWidth = 0.1f;

        private List<GameObject> laserObjList = new List<GameObject>();

        void IScanModeLaserManageable.LaserInit(List<ScanModeLaserTargetInfo> laserInfoList)
        {
            for (int i = 0; i < laserInfoList.Count; i++)
            {
                GameObject laser = Instantiate(laserObjPrefab, this.gameObject.transform);
                laser.transform.localScale = new Vector3(laserWidth, laserWidth, laserWidth);
                laserObjList.Add(laser);
            }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ScanMode;

public class LaserTest : MonoBehaviour
{
    [Inject] private IScanModeLaserManageable laserManage;

    private List<ScanModeLaserTargetInfo> scanModeLaserTargetInfos = new List<ScanModeLaserTargetInfo>();

    [SerializeField] GameObject g0;
    [SerializeField] GameObject g1;
    [SerializeField] GameObject g2;
    [SerializeField] GameObject g3;

    void Start()
    {
        ScanModeLaserTargetInfo info0 = new ScanModeLaserTargetInfo(g0.transform, g1.transform, Color.red);
        ScanModeLaserTargetInfo info1 = new ScanModeLaserTargetInfo(g2.transform, g3.transform, Color.blue);

        scanModeLaserTargetInfos.Add(info0);
        scanModeLaserTargetInfos.Add(info1);

        laserManage.LaserInit(scanModeLaserTargetInfos);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            laserManage.ShowLaser();
        }

        if (Input.GetMouseButtonDown(1))
        {
            laserManage.HideLaser();
        }
    }
}

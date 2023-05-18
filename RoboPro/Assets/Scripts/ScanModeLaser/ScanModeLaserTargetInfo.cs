using UnityEngine;

namespace ScanMode
{
    public struct ScanModeLaserTargetInfo
    {
        public Transform t0;
        public Transform t1;

        public ScanModeLaserTargetInfo(Transform t0, Transform t1)
        {
            this.t0 = t0;
            this.t1 = t1;
        }
    }
}

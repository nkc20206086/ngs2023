using UnityEngine;

namespace ScanMode
{
    public struct ScanModeLaserTargetInfo
    {
        public Transform t0;
        public Transform t1;

        public Color color;

        public ScanModeLaserTargetInfo(Transform t0, Transform t1,Color color)
        {
            this.t0 = t0;
            this.t1 = t1;

            this.color = color;
        }
    }
}

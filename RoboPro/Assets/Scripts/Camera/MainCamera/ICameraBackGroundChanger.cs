using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainCamera
{
    public interface ICameraBackGroundChanger
    {
        /// <summary>
        /// 死んだときの背景の色に変更
        /// </summary>
        public void Death_BackGroundChange();

        /// <summary>
        /// 通常の背景の色に変更
        /// </summary>
        public void Default_BackGroundChange();
    }
}


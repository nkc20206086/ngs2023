using System.Collections.Generic;

namespace ScanMode
{
    interface IScanModeLaserManageable
    {
        /// <summary>
        /// 発射するレーザーを初期化する
        /// </summary>
        /// <param name="laserInfoList">レーザー情報のList</param>
        public void LaserInit(List<ScanModeLaserTargetInfo> laserInfoList);

        /// <summary>
        /// レーザーの位置を設定する
        /// </summary>
        /// <param name="laserInfoList">レーザー情報のList</param>
        public void SetLaserPos(List<ScanModeLaserTargetInfo> laserInfoList);

        /// <summary>
        /// レーザーを描画する
        /// </summary>
        public void ShowLaser();

        /// <summary>
        /// レーザーを見えなくする
        /// </summary>
        public void HideLaser();

        /// <summary>
        /// レーザーの登録を全消去
        /// </summary>
        public void ClearLaserData();
    }
}
using System.Collections.Generic;

namespace ScanMode
{
    interface IScanModeLaserManageable
    {
        /// <summary>
        /// ���˂��郌�[�U�[������������
        /// </summary>
        /// <param name="laserInfoList">���[�U�[����List</param>
        public void LaserInit(List<ScanModeLaserTargetInfo> laserInfoList);

        /// <summary>
        /// ���[�U�[�̈ʒu��ݒ肷��
        /// </summary>
        /// <param name="laserInfoList">���[�U�[����List</param>
        public void SetLaserPos(List<ScanModeLaserTargetInfo> laserInfoList);

        /// <summary>
        /// ���[�U�[��`�悷��
        /// </summary>
        public void ShowLaser();

        /// <summary>
        /// ���[�U�[�������Ȃ�����
        /// </summary>
        public void HideLaser();

        /// <summary>
        /// ���[�U�[�̓o�^��S����
        /// </summary>
        public void ClearLaserData();
    }
}
using UnityEngine;

namespace DeathCamera
{
    /// <summary>
    /// �f�X�J�����𐧌䂷��
    /// </summary>
    interface IDeathCameraSettable
    {
        /// <summary>
        // DeathCamera��������
        /// </summary>
        void InitDeathCameraSetting();

        /// <summary>
        /// �f�X�J�����̗L��������؂�ւ���
        /// </summary>
        /// <param name="enable">�L���A����</param>
        void DeathCameraEnable(bool enable);

        /// <summary>
        /// �f�X�J�����ŕ`�悷��
        /// </summary>
        /// <param name="playerRenderer">�v���C���[��SkinnedMeshRenderer</param>
        void DrawingByDeathCamera(SkinnedMeshRenderer playerRenderer);

        /// <summary>
        /// �f�X�J�����ł̕`����~�߂�
        /// </summary>
        /// <param name="playerRenderer">�v���C���[��SkinnedMeshRenderer</param>
        void StopDrawingByDeathCamera(SkinnedMeshRenderer playerRenderer);
    }
}
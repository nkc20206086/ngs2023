using UnityEngine;

namespace DeathCamera
{
    /// <summary>
    /// デスカメラを制御する
    /// </summary>
    interface IDeathCameraSettable
    {
        /// <summary>
        // DeathCameraを初期化
        /// </summary>
        void InitDeathCameraSetting();

        /// <summary>
        /// デスカメラの有効無効を切り替える
        /// </summary>
        /// <param name="enable">有効、無効</param>
        void DeathCameraEnable(bool enable);

        /// <summary>
        /// デスカメラで描画する
        /// </summary>
        /// <param name="playerRenderer">プレイヤーのSkinnedMeshRenderer</param>
        void DrawingByDeathCamera(SkinnedMeshRenderer playerRenderer);

        /// <summary>
        /// デスカメラでの描画を止める
        /// </summary>
        /// <param name="playerRenderer">プレイヤーのSkinnedMeshRenderer</param>
        void StopDrawingByDeathCamera(SkinnedMeshRenderer playerRenderer);
    }
}
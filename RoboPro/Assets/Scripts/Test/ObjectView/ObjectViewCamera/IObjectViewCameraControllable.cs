using UnityEngine;

namespace ObjectView
{
    public interface IObjectViewCameraControllable
    {
        /// <summary>
        /// カメラの位置を設定する
        /// </summary>
        /// <param name="targetTransform">対象のTransform</param>
        public void SetCameraPos(Transform targetTransform);
        
        // TODO : カメラ担当に任せる。カメラを回転させるプログラムを作成する
        public void SetCameraRotate(Vector3 targetPos, float angle);
    }
}


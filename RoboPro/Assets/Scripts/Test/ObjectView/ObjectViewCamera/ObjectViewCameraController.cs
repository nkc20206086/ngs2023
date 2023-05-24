using UnityEngine;

namespace ObjectView
{
    public class ObjectViewCameraController : MonoBehaviour, IObjectViewCameraControllable
    {
        /// <summary>
        /// 対象までの距離
        /// </summary>
        private readonly float cameraDistance = 10f;

        /// <summary>
        /// 最初に向いてる角度
        /// </summary>
        private readonly Vector3 startDir = new Vector3(1f, 1f, 1f);

        [SerializeField] 
        private GameObject cameraObj;

        /// <summary>
        /// カメラの位置を設定する
        /// </summary>
        /// <param name="targetTransform">対象のTransform</param>
        public void SetCameraPos(Transform targetTransform)
        {
            cameraObj.transform.position = targetTransform.transform.position;
            cameraObj.transform.position += startDir * cameraDistance;
            cameraObj.transform.LookAt(targetTransform);
        }

        // TODO : カメラ担当に任せる。カメラを回転させるプログラムを作成する
        void IObjectViewCameraControllable.SetCameraRotate(Vector3 targetPos, float angle)
        {
            cameraObj.transform.RotateAround(targetPos, Vector3.up, angle);
        }
    }
}

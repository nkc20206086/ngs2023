using UnityEngine;

namespace MainCamera
{
    public interface ICameraVectorGetter
    {
        /// <summary>
        /// カメラのY値を渡す
        /// </summary>
        /// <returns></returns>
        public Vector3 VectorYGetter();

        /// <summary>
        /// カメラのX値を渡す
        /// </summary>
        /// <returns></returns>
        public Vector3 VectorXGetter();
    }
}
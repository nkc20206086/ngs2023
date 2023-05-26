using UnityEngine;

namespace MainCamera
{
    public interface ICameraVectorGetter
    {
        /// <summary>
        /// �J������Y�l��n��
        /// </summary>
        /// <returns></returns>
        public Vector3 VectorYGetter();

        /// <summary>
        /// �J������X�l��n��
        /// </summary>
        /// <returns></returns>
        public Vector3 VectorXGetter();
    }
}
using UnityEngine;

namespace MainCamera
{
    interface ICameraVectorGetter
    {
        public Vector3 VectorYGetter();
        public Vector3 VectorXGetter();
    }
}
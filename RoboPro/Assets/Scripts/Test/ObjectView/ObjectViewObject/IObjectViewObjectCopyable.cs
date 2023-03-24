using UnityEngine;

namespace ObjectView
{
    interface IObjectViewObjectCopyable
    {
        /// <summary>
        /// �I�u�W�F�N�g�r���[�ŕ\�������I�u�W�F�N�g�𐶐�����
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">�I�u�W�F�N�g��</param>
        /// <param name="transform">transform</param>
        public GameObject MakeObjectCopy(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName, Transform transform);
    }
}
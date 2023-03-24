using UnityEngine;

namespace ObjectView
{
    interface IObjectViewObjectCopyable
    {
        /// <summary>
        /// オブジェクトビューで表示されるオブジェクトを生成する
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">オブジェクト名</param>
        /// <param name="transform">transform</param>
        public GameObject MakeObjectCopy(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName, Transform transform);
    }
}
using UnityEngine;

namespace ObjectView
{
    interface IObjectViewObjectControllable
    {
        /// <summary>
        /// オブジェクトの見た目を生成する
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">オブジェクトの名前</param>
        /// <param name="objTransform">transform</param>
        public void MakeObjectViewObj(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName, Transform objTransform);
    }
}
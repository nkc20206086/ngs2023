using UnityEngine;

namespace ObjectView
{
    interface IObjectViewObjectControllable
    {
        /// <summary>
        /// �I�u�W�F�N�g�̌����ڂ𐶐�����
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">�I�u�W�F�N�g�̖��O</param>
        /// <param name="objTransform">transform</param>
        public void MakeObjectViewObj(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName, Transform objTransform);
    }
}
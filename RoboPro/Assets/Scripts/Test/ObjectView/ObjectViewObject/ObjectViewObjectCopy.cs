using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectView
{
    public class ObjectViewObjectCopy
    {
        private readonly string copiedObjSuffixName = "_ObjectViewCopy";

        /// <summary>
        /// オブジェクトビューで表示されるオブジェクトを生成する
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">オブジェクト名</param>
        /// <param name="initPos">生成位置</param>
        public GameObject MakeObjectCopy(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName, Transform transform)
        {
            GameObject copyObj = ObjectCopy(meshFilter, meshRenderer, objName);
            SetCopyObjLayer(copyObj);
            copyObj.transform.position = transform.position;
            copyObj.transform.eulerAngles = transform.eulerAngles;
            copyObj.transform.localScale = transform.localScale;
            return copyObj;
        }

        /// <summary>
        /// オブジェクトの見た目のみを複製する
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">オブジェクト名</param>
        private GameObject ObjectCopy(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName)
        {
            GameObject copyObj = new GameObject(objName + copiedObjSuffixName);

            copyObj.AddComponent<MeshFilter>().CopyFrom(meshFilter);
            copyObj.AddComponent<MeshRenderer>().CopyFrom(meshRenderer);
            return copyObj;
        }

        private void SetCopyObjLayer(GameObject obj)
        {
            obj.layer = LayerMask.NameToLayer("ObjectView");
        }
    }
}

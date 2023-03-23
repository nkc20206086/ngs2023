using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ObjectView
{
    public class ObjectViewObjectCopy
    {
        /// <summary>
        /// �I�u�W�F�N�g�̌����ڂ݂̂𕡐�����
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">objName</param>
        public GameObject ObjectCopy(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName)
        {
            GameObject copyObj = new GameObject(objName);

            copyObj.AddComponent<MeshFilter>().CopyFrom(meshFilter);
            copyObj.AddComponent<MeshRenderer>().CopyFrom(meshRenderer);
            return copyObj;
        }
    }
}

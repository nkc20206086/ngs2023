using UnityEngine;

namespace ObjectView
{
    /// <summary>
    /// オブジェクトビューでオブジェクト見た目を生成するスクリプト
    /// </summary>
    public class ObjectViewObjectController : MonoBehaviour
    {
        [SerializeField]
        private ObjectViewCameraController cameraController;

        [SerializeField]
        private GameObject axisObj;

        private ObjectViewObjectCopy objectCopy;

        private GameObject copyObj;

        private void Start()
        {
            objectCopy = new ObjectViewObjectCopy();
        }

        /// <summary>
        /// オブジェクトの見た目を生成する
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">オブジェクトの名前</param>
        /// <param name="objTransform">transform</param>
        public void MakeObjectViewObj(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName, Transform objTransform)
        {
            copyObj = objectCopy.MakeObjectCopy(meshFilter, meshRenderer, objName, objTransform);
            axisObj.transform.position = copyObj.transform.position;
            cameraController.SetCameraPos(copyObj.transform);
        }
    }
}

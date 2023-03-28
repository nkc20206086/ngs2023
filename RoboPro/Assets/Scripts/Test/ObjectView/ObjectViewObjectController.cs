using UnityEngine;

namespace ObjectView
{
    /// <summary>
    /// �I�u�W�F�N�g�r���[�ŃI�u�W�F�N�g�����ڂ𐶐�����X�N���v�g
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
        /// �I�u�W�F�N�g�̌����ڂ𐶐�����
        /// </summary>
        /// <param name="meshFilter">meshFilter</param>
        /// <param name="meshRenderer">meshRenderer</param>
        /// <param name="objName">�I�u�W�F�N�g�̖��O</param>
        /// <param name="objTransform">transform</param>
        public void MakeObjectViewObj(MeshFilter meshFilter, MeshRenderer meshRenderer, string objName, Transform objTransform)
        {
            copyObj = objectCopy.MakeObjectCopy(meshFilter, meshRenderer, objName, objTransform);
            axisObj.transform.position = copyObj.transform.position;
            cameraController.SetCameraPos(copyObj.transform);
        }
    }
}

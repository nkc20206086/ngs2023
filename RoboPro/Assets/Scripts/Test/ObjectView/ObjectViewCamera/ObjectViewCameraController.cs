using UnityEngine;

namespace ObjectView
{
    public class ObjectViewCameraController : MonoBehaviour, IObjectViewCameraControllable
    {
        /// <summary>
        /// �Ώۂ܂ł̋���
        /// </summary>
        private readonly float cameraDistance = 10f;

        /// <summary>
        /// �ŏ��Ɍ����Ă�p�x
        /// </summary>
        private readonly Vector3 startDir = new Vector3(1f, 1f, 1f);

        [SerializeField] 
        private GameObject cameraObj;

        /// <summary>
        /// �J�����̈ʒu��ݒ肷��
        /// </summary>
        /// <param name="targetTransform">�Ώۂ�Transform</param>
        public void SetCameraPos(Transform targetTransform)
        {
            cameraObj.transform.position = targetTransform.transform.position;
            cameraObj.transform.position += startDir * cameraDistance;
            cameraObj.transform.LookAt(targetTransform);
        }

        // TODO : �J�����S���ɔC����B�J��������]������v���O�������쐬����
        void IObjectViewCameraControllable.SetCameraRotate(Vector3 targetPos, float angle)
        {
            cameraObj.transform.RotateAround(targetPos, Vector3.up, angle);
        }
    }
}

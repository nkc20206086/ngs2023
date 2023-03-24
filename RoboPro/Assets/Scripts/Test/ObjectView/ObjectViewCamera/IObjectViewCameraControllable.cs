using UnityEngine;

namespace ObjectView
{
    public interface IObjectViewCameraControllable
    {
        /// <summary>
        /// �J�����̈ʒu��ݒ肷��
        /// </summary>
        /// <param name="targetTransform">�Ώۂ�Transform</param>
        public void SetCameraPos(Transform targetTransform);
        
        // TODO : �J�����S���ɔC����B�J��������]������v���O�������쐬����
        public void SetCameraRotate(Vector3 targetPos, float angle);
    }
}


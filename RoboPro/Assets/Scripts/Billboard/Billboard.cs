using UnityEngine;

namespace UI_Utility
{
    /// <summary>
    /// �I�u�W�F�N�g����ɃJ�����̕�������悤�ɂ���
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        [SerializeField]
        private Camera targetCamera;

        [Header("Lock Ratation")]

        [SerializeField]
        private bool lockX;

        [SerializeField]
        private bool lockY;

        [SerializeField]
        private bool lockZ;

        private Vector3 startRotation;

        void Start()
        {
            targetCamera ??= Camera.main;
            startRotation = transform.rotation.eulerAngles;
        }

        void Update()
        {
            transform.forward = targetCamera.transform.forward;

            LockAxis();
        }

        /// <summary>
        /// ���ł̉�]���Œ肷��
        /// </summary>
        private void LockAxis()
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            if (lockX)
            {
                rotation.x = startRotation.x;
            }
            if (lockY)
            {
                rotation.y = startRotation.y;
            }
            if (lockZ)
            {
                rotation.z = startRotation.z;
            }
            if (lockX || lockY || lockZ)
            {
                transform.rotation = Quaternion.Euler(rotation);
            }
        }
    }
}

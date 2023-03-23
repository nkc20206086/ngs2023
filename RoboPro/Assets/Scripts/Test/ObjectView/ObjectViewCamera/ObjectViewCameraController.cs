using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectView
{
    public class ObjectViewCameraController : MonoBehaviour
    {
        private const float cameraDistance = 10f;
        private Vector3 startDir = new Vector3(1f, 1f, 1f);

        [SerializeField] 
        private GameObject cameraObj;

        private void Start()
        {
            
        }

        public void SetCameraPos(Transform targetTransform)
        {
            cameraObj.transform.position = targetTransform.transform.position;
            cameraObj.transform.position += startDir * cameraDistance;
            cameraObj.transform.LookAt(targetTransform);
        }

        // TODO : ÉJÉÅÉâíSìñÇ…îCÇπÇÈ
        public void SetCameraRotate(Vector3 targetPos, float angle)
        {
            cameraObj.transform.RotateAround(targetPos, Vector3.up, angle);
        }
    }
}

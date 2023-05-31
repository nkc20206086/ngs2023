using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace AxisCamera
{
    public class AxisCameraSetting : MonoBehaviour, IAxisCameraSettable
    {
        [SerializeField]
        private GameObject axisObj;

        private Camera mainCamera;
        private Camera axisCamera;
        private ReactiveProperty<Quaternion> mainCameraRot = new ReactiveProperty<Quaternion>();

        void Start()
        {
            mainCamera = transform.parent.GetComponent<Camera>();
            axisCamera = GetComponent<Camera>();

            mainCameraRot.Value = mainCamera.gameObject.transform.rotation;
            mainCameraRot.Subscribe(r => axisObj.transform.localRotation = Quaternion.Inverse(r))
                         .AddTo(gameObject);
        }

        private void Update()
        {
            mainCameraRot.Value = mainCamera.gameObject.transform.rotation;
        }

        void IAxisCameraSettable.ShowAxisUI()
        {
            axisCamera.enabled = true;
        }

        void IAxisCameraSettable.HideAxisUI()
        {
            axisCamera.enabled = false;
        }
    }
}

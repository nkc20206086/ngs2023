using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainCamera
{
    public class CameraBackGroundChanger : MonoBehaviour,ICameraBackGroundChanger
    {
        [SerializeField]
        private Color defaultBackColor;
        [SerializeField]
        private Color deathBackColor;

        private Camera cameraObj;

        void Start()
        {
            cameraObj = GetComponent<Camera>();
        }

        void ICameraBackGroundChanger.Death_BackGroundChange()
        {
            cameraObj.backgroundColor = deathBackColor;
        }

        void ICameraBackGroundChanger.Default_BackGroundChange()
        {
            cameraObj.backgroundColor = defaultBackColor;
        }
    }
}
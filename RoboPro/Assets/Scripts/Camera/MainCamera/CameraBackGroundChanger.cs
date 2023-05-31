using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainCamera
{
    public class CameraBackGroundChanger : MonoBehaviour,ICameraBackGroundChanger
    {
        [SerializeField]
        private Color deathBackColor;

        private Color defaultBackColor;
        private Camera cameraObj;

        void Start()
        {
            cameraObj = GetComponent<Camera>();
            defaultBackColor = cameraObj.backgroundColor;
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
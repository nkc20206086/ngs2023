using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace MainCamera
{
    public class CameraClickMove : MonoBehaviour
    {
        private InputControls inputActions;
        private CinemachineInputProvider cinemachineInputProvider;
        [SerializeField]
        InputActionReference actionReference;

        // Start is called before the first frame update
        void Start()
        {
            cinemachineInputProvider = GetComponent<CinemachineInputProvider>();
            inputActions = new InputControls();
            inputActions.Enable();

            //右クリックされたときにInputActionを入れてカメラを動かす
            inputActions.MainCamera.Click.performed += context => cinemachineInputProvider.XYAxis = actionReference;
            inputActions.MainCamera.Click.canceled += context => cinemachineInputProvider.XYAxis = null;
        }
    }
}
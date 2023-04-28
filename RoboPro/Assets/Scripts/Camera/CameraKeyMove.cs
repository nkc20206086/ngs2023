using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace MainCamera
{
    public class CameraKeyMove : MonoBehaviour
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

            //�㉺���E�L�[�������ꂽ�Ƃ���InputAction�����ăJ�����𓮂���
            inputActions.MainCamera.Move.performed += context => cinemachineInputProvider.XYAxis = actionReference;
            inputActions.MainCamera.Move.canceled += context => cinemachineInputProvider.XYAxis = null;
        }
    }
}

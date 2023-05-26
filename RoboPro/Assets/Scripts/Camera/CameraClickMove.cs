using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace MainCamera
{
    public class CameraClickMove : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference actionReference;

        [SerializeField]
        private CinemachineFreeLook freeLook;

        //�Y�[���A�E�g���ꂽ�Ƃ��̒l
        [SerializeField]
        private CinemachineFreeLook.Orbit[] startOrbit;

        //�Y�[���C�����ꂽ�Ƃ��̒l
        [SerializeField]
        private CinemachineFreeLook.Orbit[] endOrbit;

        //�Y�[�����x
        [SerializeField]
        private float zoomSpeed = 0.012f;

        //�J�������n�ʂɂ߂荞�܂Ȃ��悤�ɃY�[���̍ő�l�����߂�
        [SerializeField]
        private float maxZoomValue = 0.5f;

        private InputControls inputActions;
        private CinemachineInputProvider cinemachineInputProvider;
        private float wheelValue;

        private void Start()
        {
            cinemachineInputProvider = GetComponent<CinemachineInputProvider>();
            inputActions = new InputControls();
            inputActions.Enable();

            //�E�N���b�N���ꂽ�Ƃ���InputAction�����ăJ�����𓮂���
            inputActions.MainCamera.Click.performed += context => cinemachineInputProvider.XYAxis = actionReference;
            inputActions.MainCamera.Click.canceled += context => cinemachineInputProvider.XYAxis = null;
        }

        private void Update()
        {
            //�}�E�X�z�C�[���ŃY�[���C���Y�[���A�E�g
            float mouseWheel = Mouse.current.scroll.ReadValue().y;
            wheelValue += mouseWheel * zoomSpeed * Time.deltaTime;
            wheelValue = Mathf.Clamp(wheelValue, 0, maxZoomValue);

            for (int i = 0; i < freeLook.m_Orbits.Length; i++)
            {
                float radius = Mathf.Lerp(startOrbit[i].m_Radius, endOrbit[i].m_Radius, wheelValue);
                float height = Mathf.Lerp(startOrbit[i].m_Height, endOrbit[i].m_Height, wheelValue);
                
                freeLook.m_Orbits[i] = new CinemachineFreeLook.Orbit(height, radius);
            }
        }
    }
}
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

        //ズームアウトされたときの値
        [SerializeField]
        private CinemachineFreeLook.Orbit[] startOrbit;

        //ズームインされたときの値
        [SerializeField]
        private CinemachineFreeLook.Orbit[] endOrbit;

        //ズーム速度
        [SerializeField]
        private float zoomSpeed = 0.012f;

        //カメラが地面にめり込まないようにズームの最大値を決める
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

            //右クリックされたときにInputActionを入れてカメラを動かす
            inputActions.MainCamera.Click.performed += context => cinemachineInputProvider.XYAxis = actionReference;
            inputActions.MainCamera.Click.canceled += context => cinemachineInputProvider.XYAxis = null;
        }

        private void Update()
        {
            //マウスホイールでズームインズームアウト
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
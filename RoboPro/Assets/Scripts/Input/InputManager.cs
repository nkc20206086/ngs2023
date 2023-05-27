using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace Inputs
{
    public class InputManager : InputControls.IPlayerActions
    {
        private InputControls inputActions;
        private InputControls.PlayerActions playerActions;

        public InputManager()
        {
            inputActions = new InputControls();
            inputActions.Enable();

            playerActions = new InputControls.PlayerActions(inputActions);
            playerActions.SetCallbacks(this);
        }

        void InputControls.IPlayerActions.OnInteract(InputAction.CallbackContext context)
        {
        }

        void InputControls.IPlayerActions.OnMove(InputAction.CallbackContext context)
        {
        }

        /// <summary>
        /// �ړ��L�[�̓��͒l
        /// </summary>
        public Vector2 MoveReadValue()
        {
            return inputActions.Player.Move.ReadValue<Vector2>();
        }

        /// <summary>
        /// �ړ���������Ă���Ԃ�����True��Ԃ�
        /// </summary>
        public bool IsMove()
        {
            return inputActions.Player.Move.IsPressed();
        }

        /// <summary>
        /// �C���^���N�g�{�^���������ꂽ��True����x�����Ԃ�
        /// </summary>
        public bool IsInteractPerformed()
        {
            return inputActions.Player.Interact.WasPerformedThisFrame();
        }

        /// <summary>
        /// �C���^���N�g�{�^����������Ă�Ԃ�����True��Ԃ�
        /// </summary>
        public bool IsInteractPressed()
        {
            return inputActions.Player.Interact.IsPressed();
        }

        public void SetMainCameraClickPerformed(Action action)
        {
            inputActions.MainCamera.Click.performed += context => action();
        }

        public void SetMainCameraClickCanceled(Action action)
        {
            inputActions.MainCamera.Click.canceled += context => action();
        }

        public void SetMainCameraMovePerformed(Action action)
        {
            inputActions.MainCamera.Move.performed += context => action();
        }

        public void SetMainCameraMoveCanceled(Action action)
        {
            inputActions.MainCamera.Move.canceled += context => action();
        }
    }

}

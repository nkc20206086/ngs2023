using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        private PlayerCore playerCore;
        private PlayerStay playerStay;
        private PlayerMove playerMove;
        private PlayerAccess playerAccess;
        private PlayerDizzy playerDizzy;
        private PlayerStepOff playerStepOff;
        private PlayerFall playerFall;
        private PlayerLanding playerLanding;
        private IStateGetter stateGetter;

        private InputControls inputActions;
        private Vector2 inputVec;
        private bool isMove;
        private bool isInteract;

        // Start is called before the first frame update
        void Start()
        {
            playerCore = GetComponent<PlayerCore>();
            playerStay = GetComponent<PlayerStay>();
            playerMove = GetComponent<PlayerMove>();
            playerAccess = GetComponent<PlayerAccess>();
            playerDizzy = GetComponent<PlayerDizzy>();
            playerStepOff = GetComponent<PlayerStepOff>();
            playerFall = GetComponent<PlayerFall>();
            playerLanding = GetComponent<PlayerLanding>();
            stateGetter = GetComponent<IStateGetter>();

            inputActions = new InputControls();
            inputActions.Enable();

            
        }

        private void Update()
        {
            inputVec = inputActions.Player.Move.ReadValue<Vector2>();
            //Debug.Log(inputVec);

            //ƒ{ƒ^ƒ“‚ð‰Ÿ‚³‚ê‚Ä‚¢‚é‚©”»•Ê
            isMove = inputActions.Player.Move.IsPressed();
            isInteract = inputActions.Player.Interact.WasPressedThisFrame();
        }

        private void FixedUpdate()
        {
            Debug.Log(stateGetter.StateGetter());
            //Statemachine
            switch (stateGetter.StateGetter())
            {
                case PlayerStateEnum.Stay:
                    {
                        playerStay.Act_Stay(isMove, isInteract);
                        break;
                    }
                case PlayerStateEnum.Move:
                    {
                        playerMove.Act_Move(isMove,isInteract,inputVec);
                        break;
                    }
                case PlayerStateEnum.Dizzy:
                    {
                        playerDizzy.Act_Dizzy(isMove,isInteract);
                        break;
                    }
                case PlayerStateEnum.StepOff:
                    {
                        playerStepOff.Act_StepOff();
                        break;
                    }
                case PlayerStateEnum.Fall:
                    {
                        playerFall.Act_Fall();
                        break;
                    }
                case PlayerStateEnum.ThroughFall:
                    {
                        playerFall.Act_ThroughFall();
                        break;
                    }
                case PlayerStateEnum.Falling:
                    {
                        playerFall.Act_Falling();
                        break;
                    }
                case PlayerStateEnum.Landing:
                    {
                        playerLanding.Act_Landing();
                        break;
                    }
                case PlayerStateEnum.Access:
                    {
                        playerAccess.Act_Access();
                        break;
                    }
                case PlayerStateEnum.Die:
                    {
                        break;
                    }
            }
        }
    }
}


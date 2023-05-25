using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        private PlayerStay playerStay;
        private PlayerMove playerMove;
        private PlayerAccess playerAccess;
        private PlayerDizzy playerDizzy;
        private PlayerStepOff playerStepOff;
        private PlayerFall playerFall;
        private PlayerLanding playerLanding;
        private PlayerLadderStepOn playerLadderStepOn;
        private PlayerLadderClimb playerLadderClimb;
        private PlayerFinishLadderClimb playerFinishLadderClimb;
        private PlayerDie playerDie;
        private IStateGetter stateGetter;

        private InputControls inputActions;
        Vector3 defaultScale = Vector3.zero;
        private Vector2 inputVec;
        private bool isMove;
        private bool isInteract;

        // Start is called before the first frame update
        void Start()
        {
            defaultScale = transform.lossyScale;

            playerStay = GetComponent<PlayerStay>();
            playerMove = GetComponent<PlayerMove>();
            playerAccess = GetComponent<PlayerAccess>();
            playerDizzy = GetComponent<PlayerDizzy>();
            playerStepOff = GetComponent<PlayerStepOff>();
            playerFall = GetComponent<PlayerFall>();
            playerLanding = GetComponent<PlayerLanding>();
            playerLadderStepOn = GetComponent<PlayerLadderStepOn>();
            playerLadderClimb = GetComponent<PlayerLadderClimb>();
            playerFinishLadderClimb = GetComponent<PlayerFinishLadderClimb>();
            playerDie = GetComponent<PlayerDie>();
            stateGetter = GetComponent<IStateGetter>();

            inputActions = new InputControls();
            inputActions.Enable();
        }

        private void Update()
        {
            inputVec = inputActions.Player.Move.ReadValue<Vector2>();

            //ボタンを押されているか判別
            isMove = inputActions.Player.Move.IsPressed();
            isInteract = inputActions.Player.Interact.WasPressedThisFrame();

            //足元のオブジェクトを親オブジェクトにする
            stateGetter.GroundCheckGetter().CheckParentGround();
            //DefaultScaleCalc();
        }

        private void FixedUpdate()
        {
            //Debug.Log(stateGetter.StateGetter());

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
                case PlayerStateEnum.LadderStepOn_Climb:
                    {
                        playerLadderStepOn.Act_StepOn();
                        break;
                    }
                case PlayerStateEnum.LaddderClimb:
                    {
                        playerLadderClimb.Act_Climb();
                        break;
                    }
                case PlayerStateEnum.LadderFinish_Climb:
                    {
                        playerFinishLadderClimb.Act_FinishClimb();
                        break;
                    }
                case PlayerStateEnum.Die:
                    {
                        playerDie.Act_Die();
                        break;
                    }
            }
            //AnyStateとしてどのStateでも処理を行う
            DefaultScaleCalc();
        }

        /// <summary>
        /// LocalScaleを計算する関数
        /// </summary>
        private void DefaultScaleCalc()
        {
            Vector3 lossScale = transform.lossyScale;
            Vector3 localScale = transform.localScale;

            //プレイヤーのLocalScaleを常に均一にする
            transform.localScale = new Vector3(
                    localScale.x / lossScale.x * defaultScale.x,
                    localScale.y / lossScale.y * defaultScale.y,
                    localScale.z / lossScale.z * defaultScale.z);
        }
    }
}


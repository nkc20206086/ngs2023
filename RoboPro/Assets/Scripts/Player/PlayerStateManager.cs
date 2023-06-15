using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Inputs;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        [Inject]
        private InputManager inputManager;

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
        private PlayerGoalJump playerGoal;
        private PlayerGoalDance playerGoalDance;
        private PlayerDie playerDie;
        private IStateGetter stateGetter;

        Vector3 defaultScale = Vector3.zero;
        Vector3 defaultLocalScale = Vector3.zero;
        private Vector2 inputVec;
        private bool isMove;
        private bool isInteract;

        // Start is called before the first frame update
        void Start()
        {
            defaultScale = gameObject.transform.lossyScale;

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
            playerGoal = GetComponent<PlayerGoalJump>();
            playerGoalDance = GetComponent<PlayerGoalDance>();
            playerDie = GetComponent<PlayerDie>();
            stateGetter = GetComponent<IStateGetter>();
        }

        private void Update()
        {
            inputVec = inputManager.MoveReadValue();

            //ボタンを押されているか判別
            isMove = inputManager.IsMove();
            isInteract = inputManager.IsInteractPerformed();

            //足元のオブジェクトを親オブジェクトにする
            stateGetter.GroundCheckGetter().CheckParentGround();
            DefaultScaleCalc();
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
                        if(inputVec.x != 0)
                        {
                            if(inputVec.x > 0)
                            {
                                inputVec.x = 1;
                            }
                            else
                            {
                                inputVec.x = -1;
                            }
                            inputVec.y = 0;
                        }

                        if(inputVec.y != 0)
                        {
                            if (inputVec.y > 0)
                            {
                                inputVec.y = 1;
                            }
                            else
                            {
                                inputVec.y = -1;
                            }
                            inputVec.x = 0;
                        }
                        playerMove.Act_Move(isMove, isInteract, inputVec);
                        break;
                    }
                case PlayerStateEnum.Dizzy:
                    {
                        playerDizzy.Act_Dizzy(isMove, isInteract);
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
                        playerLanding.Act_Landing(isMove);
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
                case PlayerStateEnum.Goal_Jump:
                    {
                        playerGoal.Act_GoTo_Goal();
                        break;
                    }
                case PlayerStateEnum.Goal_Dance:
                    {
                        playerGoalDance.Act_GoalDance();
                        break;
                    }
                case PlayerStateEnum.Die:
                    {
                        playerDie.Act_Die();
                        break;
                    }
            }
            //AnyStateとしてどのStateでも処理を行う

        }

        /// <summary>
        /// LocalScaleを計算する関数
        /// </summary>
        private void DefaultScaleCalc()
        {
            if (transform.parent == null)
            {
                defaultScale = transform.lossyScale;
                transform.localScale = defaultScale;

                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                var local = transform.localScale;
                var lossy = transform.lossyScale;

                transform.localScale = new Vector3(
                    (local.x / lossy.x) * defaultScale.x,
                    (local.y / lossy.y) * defaultScale.y,
                    (local.z / lossy.z) * defaultScale.z);
                //Transform parent = gameObject.transform.parent;
                //var culScale = Quaternion.Inverse(transform.rotation) * parent.localScale;
                //var signScale = new Vector3(defaultScale.x / culScale.x,
                //    defaultScale.y / culScale.y,
                //    defaultScale.z / culScale.z);
                //transform.localScale = new Vector3(Mathf.Abs(signScale.x), Mathf.Abs(signScale.y), Mathf.Abs(signScale.z));
            }
        }
    }
}


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
            //defaultLocalScale = gameObject.transform.localScale;

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
            DefaultScaleCalc();
        }

        /// <summary>
        /// LocalScaleを計算する関数
        /// </summary>
        private void DefaultScaleCalc()
        {
            if (transform.parent == null) return;
            Transform parent = transform;
            while (true)
            {
                if (parent.transform.parent == null) break;
                else parent = parent.transform.parent;
            }

            transform.localScale = new Vector3(defaultScale.x / parent.lossyScale.x,
            defaultScale.y / parent.lossyScale.y,
            defaultScale.z / parent.lossyScale.z
            );


            //Vector3 lossyScale = transform.lossyScale;
            //Vector3 localScale = transform.localScale;

            ////プレイヤーのLocalScaleを常に均一にする
            //transform.localScale = new Vector3(
            //        localScale.x / lossyScale.x * defaultScale.x,
            //        localScale.y / lossyScale.y * defaultScale.y,
            //        localScale.z / lossyScale.z * defaultScale.z);

            //Debug.Log(localScale.x);
            //Debug.Log(localScale.z);
            //Debug.Log(lossyScale.z);
            //Debug.Log(lossyScale.z);

            //transform.localScale = new Vector3(
            //    1f,
            //    1f,
            //    1f);

        }
    }
}


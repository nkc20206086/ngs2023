using InteractUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerDizzy : MonoBehaviour, IStateChange
    {
        [Inject]
        private IInteractUIControllable interactUIControllable;

        [SerializeField]
        private ScriptableObject dizzyUI;

        [SerializeField]
        private ScriptableObject alertUI;

        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Dizzy(bool isMove, bool isInteract)
        {
            if (isMove)
            {
                //Debug.Log("ふらつき");
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Cliff", true);
                Vector3 pos = transform.position;
                pos.y += stateGetter.PlayerUI_OffsetYGetter();
                interactUIControllable.SetPosition(pos);
                interactUIControllable.ShowUI(ControllerType.Keyboard, (DisplayInteractCanvasAsset)dizzyUI);

                if (stateGetter.GroundCheckGetter().CheckDeathHeight())
                {
                    interactUIControllable.ShowSkullMark();
                }
                else
                {
                    if (stateGetter.PlayerAnimatorGeter().GetBool("Flg_Cliff") == false) return;
                    if (isInteract)
                    {
                        // Debug.Log("降りる");
                        stateChangeEvent(PlayerStateEnum.StepOff);
                        interactUIControllable.HideUI();
                        interactUIControllable.HideLockUI();
                    }
                    interactUIControllable.HideLockUI();
                }

                if(stateGetter.GroundCheckGetter().CheckGround() || stateGetter.GroundCheckGetter().CheckWall())
                {
                    stateChangeEvent(PlayerStateEnum.Stay);
                    stateGetter.PlayerAnimatorGeter().SetBool("Flg_Cliff", false);
                    interactUIControllable.HideUI();
                    interactUIControllable.HideLockUI();
                }
            }
            else
            {
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Cliff", false);
                interactUIControllable.HideUI();
                stateChangeEvent(PlayerStateEnum.Stay);
            }

            //床にいるかどうかを判定する
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            }
        }
    }

}
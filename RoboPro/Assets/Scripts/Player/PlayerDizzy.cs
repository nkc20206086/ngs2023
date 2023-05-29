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
                //Debug.Log("Ç”ÇÁÇ¬Ç´");
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
                    if (isInteract)
                    {
                        // Debug.Log("ç~ÇËÇÈ");
                        stateChangeEvent(PlayerStateEnum.StepOff);
                        interactUIControllable.HideUI();
                        interactUIControllable.HideLockUI();
                    }
                    interactUIControllable.HideLockUI();
                }
            }
            else
            {
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Cliff", false);
                interactUIControllable.HideUI();
                stateChangeEvent(PlayerStateEnum.Stay);
            }

            //è∞Ç…Ç¢ÇÈÇ©Ç«Ç§Ç©ÇîªíËÇ∑ÇÈ
            if (stateGetter.GroundCheckGetter().LandingCheck() == false)
            {
                stateChangeEvent(PlayerStateEnum.ThroughFall);
                stateGetter.PlayerAnimatorGeter().SetBool("Flg_Fall", true);
            }
        }
    }

}
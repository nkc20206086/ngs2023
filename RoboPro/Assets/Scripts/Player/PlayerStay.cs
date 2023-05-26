using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using InteractUI;

namespace Player
{
    public class PlayerStay : MonoBehaviour,IStateChange
    {
        [Inject]
        private IInteractUIControllable interactUIControllable;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;
        Vector3 defaultScale;

        // Start is called before the first frame update
        void Start()
        {
            defaultScale = transform.lossyScale;
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Stay(bool isMove, bool isInteract)
        {
            
            //Debug.Log("ë“Ç¬");
            if(isMove)
            {
                stateChangeEvent(PlayerStateEnum.Move);
            }

            if (stateGetter.LadderCheckGetter().LadderClimbCheck())
            {
                if(isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderStepOn_Climb);
                }
            }

            if(stateGetter.LadderCheckGetter().LadderDownCheck())
            {
                if (isInteract)
                {
                    stateChangeEvent(PlayerStateEnum.LadderDown);
                }
            }

            int index = stateGetter.GimmickAccessGetter().GetAccessPointIndex(transform.position);
            if (index >= 0)
            {
                Vector3 pos = stateGetter.GimmickAccessGetter().Access(index);
                interactUIControllable.SetPosition(pos);
                interactUIControllable.ShowUI(ControllerType.Keyboard, InteractKinds.ReturnKey);
                if (isInteract)
                {
                    pos.y = this.transform.position.y;
                    transform.LookAt(pos);

                    stateChangeEvent(PlayerStateEnum.Access);
                }
            }
            else
            {
                interactUIControllable.HideUI();
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
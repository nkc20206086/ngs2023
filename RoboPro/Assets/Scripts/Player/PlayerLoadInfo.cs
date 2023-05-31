using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;
using Zenject;

namespace Player
{
    public class PlayerLoadInfo : MonoBehaviour
    {
        [Inject]
        private ICameraBackGroundChanger cameraBackGroundChanger;
        private IStateGetter stateGetter;
        private PlayerSavePos playerSavePos;
        private PlayerStay playerStay;
        private PlayerDie playerDie;
        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
            playerSavePos = GetComponent<PlayerSavePos>();
            playerStay = GetComponent<PlayerStay>();
            playerDie = GetComponent<PlayerDie>();

            stateGetter.GimmickAccessGetter().SetAction(Undo_PlayerPos, Redo_PlayerPos, playerSavePos.SaveList);
        }

        /// <summary>
        /// 1Ç¬éËëOÇ…ñﬂÇ∑èàóù
        /// </summary>
        public void Undo_PlayerPos()
        {
            if(playerSavePos.callCount != 0)
            {
                playerSavePos.callCount--;
            }
            
            gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
            gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];

            if (stateGetter.StateGetter() != PlayerStateEnum.Die) return;
            playerDie.ReturnToDeath();
        }

        /// <summary>
        /// 1Ç¬Ç‚ÇËíºÇ∑èàóù
        /// </summary>
        public void Redo_PlayerPos()
        {
            playerSavePos.callCount++;
            gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
            gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];
        }
    }

}
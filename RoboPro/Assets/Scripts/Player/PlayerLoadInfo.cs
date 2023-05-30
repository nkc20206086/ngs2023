using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLoadInfo : MonoBehaviour
    {
        private IStateGetter stateGetter;
        private PlayerSavePos playerSavePos;
        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
            playerSavePos = GetComponent<PlayerSavePos>();

            stateGetter.GimmickAccessGetter().SetAction(Undo_PlayerPos, Redo_PlayerPos, playerSavePos.SaveList);
        }

        /// <summary>
        /// 1Ç¬éËëOÇ…ñﬂÇ∑èàóù
        /// </summary>
        public void Undo_PlayerPos()
        {
            playerSavePos.callCount--;
            gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
            gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];
            Debug.Log("Undo call" + playerSavePos.callCount);
        }

        /// <summary>
        /// 1Ç¬Ç‚ÇËíºÇ∑èàóù
        /// </summary>
        public void Redo_PlayerPos()
        {
            gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
            gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];
            Debug.Log("Redo call" + playerSavePos.callCount);
            playerSavePos.callCount++;
        }

        //public void Death
    }

}
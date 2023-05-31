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
        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
            playerSavePos = GetComponent<PlayerSavePos>();

            stateGetter.GimmickAccessGetter().SetAction(Undo_PlayerPos, Redo_PlayerPos, playerSavePos.SaveList);
        }

        /// <summary>
        /// 1��O�ɖ߂�����
        /// </summary>
        public void Undo_PlayerPos()
        {
            playerSavePos.callCount--;
            gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
            gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];
            Debug.Log("Undo call" + playerSavePos.callCount);

            if (stateGetter.StateGetter() != PlayerStateEnum.Die) return;
            //cameraBackGroundChanger.Default_BackGroundChange();

        }

        /// <summary>
        /// 1��蒼������
        /// </summary>
        public void Redo_PlayerPos()
        {
            playerSavePos.callCount++;
            gameObject.transform.position = playerSavePos.saveVecList[playerSavePos.callCount];
            gameObject.transform.rotation = playerSavePos.saveQuaternionsList[playerSavePos.callCount];
            Debug.Log("Redo call" + playerSavePos.callCount);
        }

        //public void Death
    }

}
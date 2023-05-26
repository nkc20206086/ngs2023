using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAccess : MonoBehaviour,IStateChange
    {
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;
        private AccessManager accessManager;

        // Start is called before the first frame update
        void Start()
        {
            accessManager = Locator<AccessManager>.GetT();
            stateGetter = GetComponent<IStateGetter>();
            accessManager.accessEndEvent += Finish_Access;
        }

        /// <summary>
        /// �A�N�Z�X�֐�
        /// </summary>
        public void Act_Access()
        {
            //Debug.Log("�A�N�Z�X�|�C���g�ɃA�N�Z�X���܂���");
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Access", true);
            
            //stateChangeEvent(PlayerStateEnum.Stay);
        }

        public void Finish_Access()
        {
            
        }
    }
}
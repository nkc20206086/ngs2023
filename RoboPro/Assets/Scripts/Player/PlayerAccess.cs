using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAccess : MonoBehaviour,IStateChange
    {
        [SerializeField]
        private AccessManager accessManager;
        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;

        // Start is called before the first frame update
        void Start()
        {
            accessManager = accessManager.GetComponent<AccessManager>();
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
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
        private Goal goal;

        // Start is called before the first frame update
        void Start()
        {
            accessManager = accessManager.GetComponent<AccessManager>();
            stateGetter = GetComponent<IStateGetter>();
            accessManager.accessEndEvent += Finish_Access;
            goal = GameObject.FindObjectOfType<Goal>();
            goal.OnClear += Goal_OnClear; 
        }

        private void Goal_OnClear()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Access", false);
            stateChangeEvent(PlayerStateEnum.Stay);
            Invoke("Access_Goal", 1);
        }

        /// <summary>
        /// アクセス関数
        /// </summary>
        public void Act_Access()
        {
            //Debug.Log("アクセスポイントにアクセスしました");
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Access", true);
            
            //stateChangeEvent(PlayerStateEnum.Stay);
        }

        public void Finish_Access()
        {
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Access", false);
            stateChangeEvent(PlayerStateEnum.Stay);
        }

        public void Access_Goal()
        {   
            stateChangeEvent(PlayerStateEnum.Goal_Jump);
        }
    }
}
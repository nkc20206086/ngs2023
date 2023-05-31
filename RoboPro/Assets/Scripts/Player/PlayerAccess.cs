using Robo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerAccess : MonoBehaviour,IStateChange
    {
        [Inject]
        private IAudioPlayer audioPlayer;

        [SerializeField]
        private AccessManager accessManager;

        [SerializeField]
        private GameObject effect;

        private IStateGetter stateGetter;
        public event Action<PlayerStateEnum> stateChangeEvent;
        private Goal goal;
        // Start is called before the first frame update
        void Start()
        {
            effect.gameObject.SetActive(false);
            accessManager = accessManager.GetComponent<AccessManager>();
            stateGetter = GetComponent<IStateGetter>();
            accessManager.accessEndEvent += Finish_Access;
            goal = GameObject.FindObjectOfType<Goal>();
            goal.OnEndInteract += Finish_Access;
            goal.OnClear += Goal_OnClear; 
        }

        private void Goal_OnClear()
        {
            audioPlayer.StopBGM();
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Access", false);
            effect.gameObject.SetActive(false);
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
            effect.gameObject.SetActive(true);
        }

        public void Finish_Access()
        {
            effect.gameObject.SetActive(false);
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Access", false);
            stateChangeEvent(PlayerStateEnum.Stay);
        }

        public void Access_Goal()
        {   
            stateChangeEvent(PlayerStateEnum.Goal_Jump);
        }
    }
}
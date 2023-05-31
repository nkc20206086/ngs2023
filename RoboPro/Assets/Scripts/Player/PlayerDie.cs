using DeathCamera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MainCamera;
using Robo;

namespace Player
{
    public class PlayerDie : MonoBehaviour, IStateChange
    {
        [Inject]
        private IAudioPlayer audioPlayer;

        [Inject]
        private IDeathCameraSettable deathCameraSettable;

        [Inject]
        private ICameraBackGroundChanger cameraBackGroundChanger;

        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        private IStateGetter stateGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;

        private bool isExplosion;

        // Start is called before the first frame update
        void Start()
        {
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Die()
        {
            if (stateGetter.CheckDeathBoolGetter() == false) return;
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Die", true);
            //Stop_BGM();
        }

        public void ReturnToDeath()
        {
            stateChangeEvent(PlayerStateEnum.Stay);
            stateGetter.PlayerAnimatorGeter().SetBool("Flg_Die", false);
            stateGetter.RigidbodyGetter().useGravity = true;
            //ReStart_BGM();
        }

        private void ReStart_BGM()
        {
            audioPlayer.PlaySE(CueSheetType.StageBGM, "BGM_Stage_01");
        }

        private void Stop_BGM()
        {
            audioPlayer.StopBGM();
        }
    }
}
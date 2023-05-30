using DeathCamera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerDie : MonoBehaviour,IStateChange
    {
        [Inject]
        private IDeathCameraSettable deathCameraSettable;

        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;
        private IStateGetter stateGetter;

        public event Action<PlayerStateEnum> stateChangeEvent;

        private PlayerEffectData playerEffect;

        private bool isExplosion;

        // Start is called before the first frame update
        void Start()
        {
            playerEffect = GetComponent<PlayerEffectData>();
            stateGetter = GetComponent<IStateGetter>();
        }

        public void Act_Die()
        {
            deathCameraSettable.DeathCameraEnable(true);
            deathCameraSettable.DrawingByDeathCamera(skinnedMeshRenderer);
            
            if (isExplosion) return;
            isExplosion = true;
            stateGetter.PlayerAnimatorGeter().SetTrigger("Trigger_Die");
            Instantiate(playerEffect.explosionEffect, gameObject.transform.position, Quaternion.identity);
            playerEffect.explosionEffect.gameObject.SetActive(true);
        }
    }
}
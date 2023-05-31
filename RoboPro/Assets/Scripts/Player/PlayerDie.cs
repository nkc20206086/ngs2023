using DeathCamera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MainCamera;

namespace Player
{
    public class PlayerDie : MonoBehaviour,IStateChange
    {
        [Inject]
        private IDeathCameraSettable deathCameraSettable;

        [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField]
        private Camera camera;
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
            
            
            if (isExplosion) return;
            isExplosion = true;
            stateGetter.PlayerAnimatorGeter().SetTrigger("Trigger_Die");


            //camera.transform.position = gameObject.transform.forward;
            //camera.transform.LookAt(gameObject.transform.position);
            //deathCameraSettable.DeathCameraEnable(true);
            //deathCameraSettable.DrawingByDeathCamera(skinnedMeshRenderer);
        }
    }
}
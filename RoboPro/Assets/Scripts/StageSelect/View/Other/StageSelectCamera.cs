using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Robo
{
    public class StageSelectCamera : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [Inject]
        private IStageSelectView view;

        private void Start()
        {
            view.OnPlay += () =>
            {
                animator.Play("StageSelectCamera_GoToStage");
            };
        }
    }

}
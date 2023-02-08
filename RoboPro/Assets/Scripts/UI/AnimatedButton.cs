using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Robo
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string clickAnimTriggerName = "OnClick";
        [SerializeField] private string enterAnimTriggerName = "OnEnter";
        [SerializeField] private string exitAnimTriggerName = "OnExit";

        public event Action OnClick;
        public event Action OnEnter;
        public event Action OnExit;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (clickAnimTriggerName != null)
                animator.SetTrigger(clickAnimTriggerName);
            OnClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enterAnimTriggerName != null)
                animator.SetTrigger(enterAnimTriggerName);
            OnEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (exitAnimTriggerName != null)
                animator.SetTrigger(exitAnimTriggerName);
            OnExit?.Invoke();
        }
    }
}
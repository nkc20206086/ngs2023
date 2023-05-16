using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Robo
{
    public class StageSelectPlayerView : MonoBehaviour
    {
        [SerializeField] 
        private float moveDuration = 0.5f;

        [SerializeField]
        private float rotateDuration = 0.2f;

        [SerializeField] 
        private Ease moveEase = Ease.Linear;

        [Inject]
        private IStageSelectView view;

        private Tween tween;

        private bool stopped;

        private void Start()
        {
            view.OnSelect += (idx) =>
            {
                MoveTo(view.Elements[idx].transform);
            };

            view.OnSelectNextKey += () =>
            {
                transform.DORotate(new Vector3(0, 90, 0), rotateDuration);
            };

            view.OnSelectPreviousKey += () =>
            {
                transform.DORotate(new Vector3(0, -90, 0), rotateDuration);
            };
        }

        private void MoveTo(Transform target)
        {
            stopped = false;
            tween?.Kill();
            transform.SetParent(target);
            tween = transform.DOLocalMove(Vector3.zero, moveDuration).SetEase(moveEase);
            tween.onComplete += () =>
            {
                stopped = true;
                transform.DORotate(new Vector3(0, 180, 0), rotateDuration);
            };
        }
    }
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Robo
{
    public class StageSelectElementView : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private Transform iconParent;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private float waitTime = 1;

        [SerializeField]
        private float moveIconDuration = 1;

        [Inject]
        private IStageSelectElementInfoView elementInfoView;

        private int index;

        public void Initalize(StageSelectElementInfo info, int index)
        {
            this.index = index;
            icon.sprite = info.StageIcon;
        }

        public void OnSelect(int idx)
        {
            if (index != idx) return;
            animator.SetBool("Selected", true);
            MoveIcon();
        }

        public void OnDeselect(int idx)
        {
            if (index != idx) return;
            animator.SetBool("Selected", false);
            icon.gameObject.SetActive(true);
            ReturnIcon();
        }

        private void MoveIcon()
        {
            icon.rectTransform.SetParent(elementInfoView.IconParent);
            icon.rectTransform.DOLocalMove(Vector3.zero, moveIconDuration).onUpdate += () =>
            {
                icon.rectTransform.localScale = Vector3.one;
                icon.rectTransform.sizeDelta = Vector2.zero;
            };

        }

        private void ReturnIcon()
        {
            icon.rectTransform.SetParent(iconParent);
            icon.rectTransform.DOLocalMove(Vector3.zero, moveIconDuration).onUpdate += () =>
            {
                icon.rectTransform.localScale = Vector3.one;
                icon.rectTransform.sizeDelta = Vector2.zero;
            };
        }
    }
}
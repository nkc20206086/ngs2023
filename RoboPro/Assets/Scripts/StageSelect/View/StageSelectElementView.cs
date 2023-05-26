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

        [SerializeField]
        private GameObject groundCleared;

        [SerializeField]
        private GameObject groundNotCleared;

        [Inject]
        private IStageSelectElementInfoView elementInfoView;

        [Inject]
        private IStageSelectView view;

        private int index;

        public void Initalize(StageSelectElementInfo info, int index)
        {
            this.index = index;
            icon.sprite = info.StageIcon;

            StageSelectElementSaveData saveData = view.SaveData.GetSaveData(info.StageNumber);
            if(saveData == null)
            {
                groundNotCleared.gameObject.SetActive(true);
                groundCleared.gameObject.SetActive(false);
            }
            else
            if (saveData.IsClear)
            {
                groundNotCleared.gameObject.SetActive(false);
                groundCleared.gameObject.SetActive(true);
            }
            else
            {
                groundNotCleared.gameObject.SetActive(true);
                groundCleared.gameObject.SetActive(false);
            }
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
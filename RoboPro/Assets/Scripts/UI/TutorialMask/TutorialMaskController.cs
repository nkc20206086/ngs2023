using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ScreenMask
{
    public class TutorialMaskController : MonoBehaviour, IScreenMaskable
    {
        [SerializeField]
        private RectTransform maskRectTrans;

        [SerializeField]
        private RectTransform canvasRectTrans;

        [SerializeField]
        private Image screenImage;

        private Vector2 maskPos;

        
        void IScreenMaskable.SetMaskFadeinMax()
        {
            maskRectTrans.sizeDelta = new Vector2(2000, 2000);
        }

        void IScreenMaskable.PlayMaskFadeinMax(float duration, Ease ease = Ease.InOutQuint)
        {
            maskRectTrans.DOSizeDelta(new Vector2(2000, 2000), duration).SetEase(ease);
        }

        void IScreenMaskable.PlayMaskFadeout(Vector2 pos, Vector2 size, float duration, Ease ease = Ease.InOutQuint)
        {
            Vector2 viewportPointPos = Camera.main.WorldToViewportPoint(pos);
            Vector2 screenPosition = new Vector2(
                ((viewportPointPos.x * canvasRectTrans.sizeDelta.x) -(canvasRectTrans.sizeDelta.x * 0.5f)),
                ((viewportPointPos.y * canvasRectTrans.sizeDelta.y) -(canvasRectTrans.sizeDelta.y * 0.5f)));
            maskPos = screenPosition;
            maskRectTrans.anchoredPosition = maskPos;
            maskRectTrans.DOSizeDelta(size, duration).SetEase(ease);
        }

        void IScreenMaskable.SetScreenColor(Color color)
        {
            screenImage.color = color;
        }
    }
}
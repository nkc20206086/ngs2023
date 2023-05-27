using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ProgramPanelView : MonoBehaviour
{
    [SerializeField]private GameObject programPanelCanvas;
    [SerializeField] private UnityEvent UICloseEvent = new UnityEvent();

    public void Start()
    {
        programPanelCanvas.transform.localScale = Vector3.zero;
        programPanelCanvas.SetActive(false);
    }
    /// <summary>
    /// キャンバスを表示する
    /// </summary>
    public void CanvasShow()
    {
        programPanelCanvas.SetActive(true);
        programPanelCanvas.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
    }

    /// <summary>
    /// キャンバスを非表示にする
    /// </summary>
    public void CanvasHide()
    {
        programPanelCanvas.transform.DOScale(Vector3.zero, 0.2f).OnComplete(CanvasCloseComplete);
    }

    private void CanvasCloseComplete()
    {
        programPanelCanvas.SetActive(false);
        UICloseEvent.Invoke();
    }
}

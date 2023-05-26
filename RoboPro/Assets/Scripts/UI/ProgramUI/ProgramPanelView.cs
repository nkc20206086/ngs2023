using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ProgramPanelView : MonoBehaviour
{
    [SerializeField]private GameObject programPanelCanvas;
    [SerializeField] private UnityEvent UICloseEvent = new UnityEvent();
    /// <summary>
    /// �L�����o�X��\������
    /// </summary>
    public void CanvasShow(bool showflg)
    {
        if (showflg)
        {
            programPanelCanvas.SetActive(true);
            programPanelCanvas.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.2f);
        }
        else
        {
            /// �L�����o�X���\���ɂ���
            programPanelCanvas.transform.DOScale(Vector3.zero, 0.2f).OnComplete(CanvasCloseComplete);
        }
    }

    private void CanvasCloseComplete()
    {
        programPanelCanvas.SetActive(false);
        UICloseEvent.Invoke();
    }
}

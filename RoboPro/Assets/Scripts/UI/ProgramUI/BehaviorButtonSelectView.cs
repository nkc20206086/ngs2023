using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BehaviorButtonSelectView : MonoBehaviour
{
    [SerializeField]
    private Image targetGrafic;

    [SerializeField]
    private Color selectingColor;
    private Color targetColor;

    private bool clickFlg = false;

    [SerializeField]
    private UnityEvent ClickEvent = new UnityEvent();    
    [SerializeField]
    private UnityEvent BehaviorSelectingEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent BehaviorUnSelectingEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent BehaviorClickEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent CreaEvent = new UnityEvent();

    private void Awake()
    {
            targetColor = targetGrafic.color;
    }

    public void SelectingButton()
    {
        if (!clickFlg)
        {
            targetGrafic.color += selectingColor;
            BehaviorSelectingEvent.Invoke();
        }
    }

    public void UnSelectingButton()
    {
        if (!clickFlg)
        {
            targetGrafic.color = targetColor;
            BehaviorUnSelectingEvent.Invoke();
        }
    }

    public void ClickButton()
    {
        BehaviorClickEvent.Invoke();
        targetGrafic.color = Color.gray;
        clickFlg = true;
        ClickEvent.Invoke();
    }

    public void SelectCrea()
    {
        CreaEvent.Invoke();
        targetGrafic.color = targetColor;
        clickFlg = false;
    }
}

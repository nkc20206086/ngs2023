using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ValueButtonView : MonoBehaviour
{
    [SerializeField]
    private Image[] targetGrafic;

    [SerializeField]
    private Color selectingColor;
    private Color targetColor;

    private bool clickFlg = false;

    [SerializeField]
    private UnityEvent ClickEvent = new UnityEvent();

    private void Awake()
    {
        targetColor = targetGrafic[0].color;
    }

    public void SelectingButton()
    {
        if (!clickFlg)
        {
            targetGrafic[0].color += selectingColor;
            targetGrafic[1].color += selectingColor;
        }
    }

    public void UnSelectingButton()
    {
        if (!clickFlg)
        {
            targetGrafic[0].color = targetColor;
            targetGrafic[1].color = targetColor;
        }
    }

    public void ClickButton()
    {
        targetGrafic[0].color = Color.gray;
        targetGrafic[1].color = Color.gray;
        clickFlg = true;
        ClickEvent.Invoke();
    }

    public void SelectCrea()
    {
        targetGrafic[0].color = targetColor;
        targetGrafic[1].color = targetColor;
        clickFlg = false;
    }

    public void BehaviorClick()
    {
        targetGrafic[0].color = Color.gray;
        targetGrafic[1].color = Color.gray;
        clickFlg = true;
    }
}

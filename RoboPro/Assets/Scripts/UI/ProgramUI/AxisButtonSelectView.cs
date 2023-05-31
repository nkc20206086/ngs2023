using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AxisButtonSelectView : MonoBehaviour
{
    [SerializeField]
    private Image targetGrafic;

    [SerializeField]
    private Color selectingColor;
    private Color targetColor;

    private bool clickFlg = false;

    [SerializeField]
    private UnityEvent ClickEvent = new UnityEvent();

    private void Awake()
    {
        targetColor = targetGrafic.color;
    }

    public void SelectingButton()
    {
        if(!clickFlg) targetGrafic.color += selectingColor;
    }

    public void UnSelectingButton()
    {
        if (!clickFlg) targetGrafic.color = targetColor;
    }

    public void ClickButton()
    {
        targetGrafic.color = Color.gray;
        clickFlg = true;
        ClickEvent.Invoke();
    }

    public void SelectCrea()
    {
        targetGrafic.color = targetColor;
        clickFlg = false;
    }

    public void BehaviorClick()
    {
        targetGrafic.color = Color.gray;
        clickFlg = true;
    }
}

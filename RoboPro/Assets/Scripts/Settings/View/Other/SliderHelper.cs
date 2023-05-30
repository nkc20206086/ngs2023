using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderHelper : MonoBehaviour, IPointerUpHandler
{
    public event Action<PointerEventData> OnEndDrag;

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        OnEndDrag?.Invoke(eventData);
    }
}

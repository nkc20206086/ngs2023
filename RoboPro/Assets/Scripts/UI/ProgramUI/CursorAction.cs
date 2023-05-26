using UnityEngine;

public class CursorAction : MonoBehaviour
{
    public void CursorShow()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void CursorHide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void CursorSelect()
    {

    }
}

using UnityEngine;

public class CursorAction : MonoBehaviour
{
    [SerializeField]
    private BehaviorButtonSelectView bbsv;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            bbsv.SelectCrea();
        }
    }
}

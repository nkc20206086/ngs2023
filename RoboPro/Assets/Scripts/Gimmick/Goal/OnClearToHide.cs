using UnityEngine;

public class OnClearToHide : MonoBehaviour
{
    private Goal goal;
    private bool setGoalEvent;

    private void Update()
    {
        if(goal != null && !setGoalEvent)
        {
            setGoalEvent = true;
            goal.OnClear += OnClear;
        }
        else
        {
            goal = FindObjectOfType<Goal>();
        }
    }

    private void OnClear()
    {
        gameObject.SetActive(false);
    }
}

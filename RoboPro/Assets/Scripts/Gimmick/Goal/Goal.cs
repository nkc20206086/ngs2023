using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool isHitPlayer = false;
    private InputControls inputActions;
    private float interactTime = 0;
    private bool isClear = false;

    public event Action<float> OnChangeInteractingTime;
    public event Action OnHitGoal;
    public event Action OnExitGoal;
    public event Action OnClear;

    private void Start()
    {
        inputActions = new InputControls();
        inputActions.Enable();
    }

    private void Update()
    {
        //プレイヤーとゴールに接触し、一秒間インタラクトするとクリアとなる
        if (isHitPlayer && inputActions.Player.Interact.IsPressed())
        {
            if (interactTime >= 1)
            {
                Clear();
            }
            else
            {
                interactTime += Time.deltaTime;
                OnChangeInteractingTime?.Invoke(interactTime);
                Debug.Log("Interact");
            }
        }
        else
        //インタラクトを解除すると、徐々に秒数が減少する
        if(!isClear)
        {
            if (interactTime > 0)
            {
                interactTime -= Time.deltaTime;
            }
            else
            {
                interactTime = 0;
            }
            OnChangeInteractingTime?.Invoke(interactTime);
        }
    }

    private void Clear()
    {
        Debug.Log("Clear");
        isClear = true;
        OnClear?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isHitPlayer = true;
            OnHitGoal?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isHitPlayer = false;
            OnExitGoal?.Invoke();
        }
    }
}

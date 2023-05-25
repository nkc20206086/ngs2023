using UnityEngine;
using System;

public class CommandPMChange : MonoBehaviour
{
    [SerializeField] private int index;
    public event Action<int> commandPMchange;

    public void PMChangeEvent()
    {
        commandPMchange(index);
    }
}

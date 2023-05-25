using UnityEngine;
using System;

public class InventryPMChange : MonoBehaviour
{
    [SerializeField] private int index;
    public event Action<int> inventryPMchange;

    public void PMChangeEvent()
    {
        inventryPMchange(index);
    }
}

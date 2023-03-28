using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractUI;

public class TestInteractUI : MonoBehaviour
{
    [SerializeField]
    private ControllerType controllerType = ControllerType.keyboard;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAA");
        InteractUIView view = other.gameObject.GetComponent<InteractUIView>();
        view.ShowUI(controllerType);
    }

    private void OnTriggerExit(Collider other)
    {
        InteractUIView view = other.gameObject.GetComponent<InteractUIView>();
        view.HideUI();
    }
}

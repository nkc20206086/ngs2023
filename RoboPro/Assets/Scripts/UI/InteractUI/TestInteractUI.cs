using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractUI;

public class TestInteractUI : MonoBehaviour
{
    [SerializeField]
    private ControllerType controllerType = ControllerType.keyboard;

    [SerializeField]
    private InteractUIController view;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            view.ShowUI(controllerType, InteractKinds.decideKey);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            view.HideUI();
        }
    }

    Vector3 offset = new Vector3(0f, 1f, 0f);
    private void OnTriggerStay(Collider other)
    {
        view.SetPosition(other.transform.position + offset);
        view.ShowUI(controllerType, InteractKinds.decideKey);
    }

    private void OnTriggerExit(Collider other)
    {
        view.HideUI();
    }
}

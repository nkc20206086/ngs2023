using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using InteractUI;

public class InteractUITest : MonoBehaviour
{
    [Inject]
    private IInteractUIControllable interact;

    [SerializeField]
    DisplayInteractCanvasAsset asset0;

    [SerializeField]
    DisplayInteractCanvasAsset asset1;

    float num = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            interact.SetPosition(new Vector3(.5f, 0f, 0f));
            interact.ShowUI(ControllerType.Keyboard, asset0);
            interact.ShowCrossMarkUI();
        }

        if(Input.GetMouseButton(0))
        {
            num += 0.001f;
            //interact.SetFillAmount(num);
        }

        if (Input.GetMouseButtonDown(1))
        {
            interact.SetPosition(new Vector3(0f, 0.5f, 0f));
            interact.ShowUI(ControllerType.Keyboard, asset1);
        }
    }
}

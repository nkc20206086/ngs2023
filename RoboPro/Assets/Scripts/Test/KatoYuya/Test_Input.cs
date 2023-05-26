using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using Zenject;

public class Test_Input : MonoBehaviour
{
    [Inject]
    private InputManager inputManager;

    void Start()
    {
        
    }

    void Update()
    {
        if(inputManager.IsInteractPerformed())
        {
            Debug.Log("AAA");
        }

        
    }

    private void Test()
    {
        Debug.Log("Test");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AxisCamera;
using Zenject;

public class Test_AxisCamera : MonoBehaviour
{
    [Inject] private IAxisCameraSettable axis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            axis.HideAxisUI();
        }

        if (Input.GetMouseButtonDown(1))
        {
            axis.ShowAxisUI();
        }
    }
}

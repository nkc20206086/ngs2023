using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    [SerializeField, Header("‰ñ“]‘¬“x")] float rotateSpeed = 0;
    void Update()
    {
        this.transform.Rotate(0,rotateSpeed, 0);
    }
}

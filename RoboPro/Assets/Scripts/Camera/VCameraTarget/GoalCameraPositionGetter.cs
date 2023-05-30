using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCameraPositionGetter : MonoBehaviour
{
    public static Vector3 GetPosition { get; private set; }
    
    public void Update()
    {
        GetPosition = transform.position;    
    }
}

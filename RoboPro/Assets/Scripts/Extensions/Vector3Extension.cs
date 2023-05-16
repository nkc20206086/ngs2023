using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Vector3Extension 
{
    public static Vector3 Floor(this Vector3 vec)
    {
        return new Vector3(Mathf.Floor(vec.x), Mathf.Floor(vec.y), Mathf.Floor(vec.z));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainCamera;

public static class CameraLocator
{
    static ICameraVectorGetter cameraVectorGetter;

    public static void Bind(ICameraVectorGetter cameraVector)
    {
        cameraVectorGetter = cameraVector;
    }

    public static ICameraVectorGetter Get()
    {
        return cameraVectorGetter;
    }
}

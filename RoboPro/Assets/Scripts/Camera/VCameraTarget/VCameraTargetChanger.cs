using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class VCameraTargetChanger : MonoBehaviour, IVCameraTargetChanger
{
    private Dictionary<VCameraType, CinemachineVirtualCameraBase> cameras = new Dictionary<VCameraType, CinemachineVirtualCameraBase>();

    public void AddCamera(VCameraType type, CinemachineVirtualCameraBase camera)
    {
        cameras.Add(type, camera);
    }

    public void RemoveCamera(VCameraType type)
    {
        cameras.Remove(type);
    }

    public void ChangeCameraTarget(VCameraType type)
    {
        foreach(CinemachineVirtualCameraBase cam in cameras.Values)
        {
            cam.Priority = 0;
        }
        cameras[type].Priority = 1;
    }
}

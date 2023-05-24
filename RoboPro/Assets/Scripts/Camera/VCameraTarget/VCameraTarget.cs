using Cinemachine;
using UnityEngine;
using Zenject;

public class VCameraTarget : MonoBehaviour
{
    [SerializeField] 
    private VCameraType vCameraType;

    [Inject]
    private IVCameraTargetChanger cameraTargetChanger;

    private CinemachineVirtualCameraBase cameraBase;

    private void Start()
    {
        cameraBase = GetComponent<CinemachineVirtualCameraBase>();
        cameraTargetChanger.AddCamera(vCameraType, cameraBase);
    }

    private void OnDestroy()
    {
        cameraTargetChanger.RemoveCamera(vCameraType);
    }
}

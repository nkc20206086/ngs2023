using Cinemachine;

public interface IVCameraTargetChanger
{
    void AddCamera(VCameraType type, CinemachineVirtualCameraBase camera);
    void RemoveCamera(VCameraType type);
    void ChangeCameraTarget(VCameraType type);
}
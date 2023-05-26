using UnityEngine;
using Zenject;

public class VCameraTargetInstaller : MonoInstaller
{
    [SerializeField] private VCameraTargetChanger vCameraTargetChanger;
    [SerializeField] private MainCamera.CameraCalcForword cameraVectorGetter;

    public override void InstallBindings()
    {
        Container.Bind<IVCameraTargetChanger>().FromInstance(vCameraTargetChanger);
        Container.Bind<MainCamera.ICameraVectorGetter>().FromInstance(cameraVectorGetter).AsCached();
    }
}
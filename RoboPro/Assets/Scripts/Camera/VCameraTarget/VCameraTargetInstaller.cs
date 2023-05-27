using UnityEngine;
using Zenject;

public class VCameraTargetInstaller : MonoInstaller
{
    [SerializeField] private VCameraTargetChanger vCameraTargetChanger;
    [SerializeField] private MainCamera.CameraCalcForword calcForword;

    public override void InstallBindings()
    {
        Container.Bind<IVCameraTargetChanger>().FromInstance(vCameraTargetChanger);
        Container.Bind<MainCamera.ICameraVectorGetter>().FromInstance(calcForword);
    }
}
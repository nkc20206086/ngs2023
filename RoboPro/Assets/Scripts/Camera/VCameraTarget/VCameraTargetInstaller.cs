using UnityEngine;
using Zenject;

public class VCameraTargetInstaller : MonoInstaller
{
    [SerializeField] private VCameraTargetChanger vCameraTargetChanger;

    public override void InstallBindings()
    {
        Container.Bind<IVCameraTargetChanger>().FromInstance(vCameraTargetChanger);
    }
}
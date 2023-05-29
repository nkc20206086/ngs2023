using UnityEngine;
using Zenject;
using DeathCamera;

public class DeathCameraInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject deathCameraObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DeathCameraSetting>()
                 .FromComponentSibling()
                 .AsCached();
    }
}
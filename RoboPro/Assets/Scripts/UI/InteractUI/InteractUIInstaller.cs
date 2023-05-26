using UnityEngine;
using Zenject;
using InteractUI;

public class InteractUIInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject interactUIObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InteractUIController>()
                 .FromComponentOn(interactUIObj)
                 .AsCached();
    }
}
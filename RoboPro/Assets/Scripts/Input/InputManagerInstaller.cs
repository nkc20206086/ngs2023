using UnityEngine;
using Zenject;
using Inputs;

public class InputManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InputManager>()
                 .AsSingle();
    }
}
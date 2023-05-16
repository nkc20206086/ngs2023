using UnityEngine;
using Zenject;
using Command;

public class CommandInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject commandSwitch;

    public override void InstallBindings()
    {
        //Container.Bind<ICommandChangeable>()
        //         .To<CommandManager>()
        //         .FromComponentOn(commandSwitch)
        //         .AsSingle();
    }
}
using UnityEngine;
using Zenject;

namespace Robo
{
    public class ScreenSettingsViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IScreenSettingsView>().To<ScreenSettingsView>().AsSingle();
            Container.Bind<ScreenSettingsPresenter>().AsSingle().NonLazy();
        }
    }
}


using Zenject;

namespace Robo
{
    public class SettingsModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAudioSettings>().To<AudioSettings>().AsSingle();
            Container.Bind<IScreenSettings>().To<ScreenSettings>().AsSingle();
            Container.Bind<ISettings>().To<Settings>().AsSingle().NonLazy();
        }
    }
}

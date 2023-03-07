using Zenject;

namespace Robo
{
    public class SystemSettingsModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAudioSettings>().To<AudioSettings>().AsSingle();
            Container.Bind<IScreenSettings>().To<ScreenSettings>().AsSingle();
            Container.Bind<ISystemSettingsControllable>().To<SystemSettings>().AsSingle().NonLazy();
        }
    }
}

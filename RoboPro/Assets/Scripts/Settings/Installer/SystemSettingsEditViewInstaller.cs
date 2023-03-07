using UnityEngine;
using Zenject;

namespace Robo
{
    public class SystemSettingsEditViewInstaller : MonoInstaller
    {
        [SerializeField]
        private SystemSettingsEditView settingsEditView;
        [SerializeField]
        private AudioSettingsEditView audioSettingsEditView;
        [SerializeField]
        private ScreenSettingsEditView screenSettingsEditView;

        public override void InstallBindings()
        {
            Container.BindInstance<ISystemSettingsEditView>(settingsEditView);
            Container.BindInstance<IAudioSettingsEditView>(audioSettingsEditView);
            Container.BindInstance<IScreenSettingsEditView>(screenSettingsEditView);

            Container.Bind<AudioSettingsEditPresenter>().AsSingle().NonLazy();
            Container.Bind<ScreenSettingsEditPresenter>().AsSingle().NonLazy();
            Container.Bind<SystemSettingsEditPresenter>().AsSingle().NonLazy();
        }
    }
}

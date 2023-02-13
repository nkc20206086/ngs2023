using UnityEngine;
using Zenject;

namespace Robo
{
    public class SettingsEditViewInstaller : MonoInstaller
    {
        [SerializeField]
        private SettingsEditView settingsEditView;
        [SerializeField]
        private AudioSettingsEditView audioSettingsEditView;
        [SerializeField]
        private ScreenSettingsEditView screenSettingsEditView;

        public override void InstallBindings()
        {
            Container.BindInstance<ISettingsEditView>(settingsEditView);
            Container.BindInstance<IAudioSettingsEditView>(audioSettingsEditView);
            Container.BindInstance<IScreenSettingsEditView>(screenSettingsEditView);

            Container.Bind<AudioSettingsEditPresenter>().AsSingle().NonLazy();
            Container.Bind<ScreenSettingsEditPresenter>().AsSingle().NonLazy();
            Container.Bind<SettingsEditPresenter>().AsSingle().NonLazy();
        }
    }
}

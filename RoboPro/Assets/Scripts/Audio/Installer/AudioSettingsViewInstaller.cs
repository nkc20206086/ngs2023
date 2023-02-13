using UnityEngine;
using Zenject;

namespace Robo
{
    public class AudioSettingsViewInstaller : MonoInstaller
    {
        [SerializeField]
        private AudioSettingsView audioSettingsView;

        public override void InstallBindings()
        {
            Container.BindInstance<IAudioSettingsView>(Instantiate(audioSettingsView, transform));
            Container.Bind<AudioSettingsPresenter>().AsSingle().NonLazy();
        }
    }
}


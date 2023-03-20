using UnityEngine;
using Zenject;

namespace Robo
{
    public class AudioInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IAudioPlayer), typeof(IInitializable)).To<AudioPlayer>().AsSingle();
            Container.Bind<AudioSettingsPresenter>().AsSingle().NonLazy();
        }
    }
}


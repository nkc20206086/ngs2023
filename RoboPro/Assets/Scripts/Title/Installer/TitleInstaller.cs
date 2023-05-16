using UnityEngine;
using Zenject;

namespace Robo
{
    public class TitleInstaller : MonoInstaller
    {
        [SerializeField] private TitleView view;

        public override void InstallBindings()
        {
            Container.Bind<ITitleModel>().To<TitleModel>().AsSingle();
            Container.BindInstance<ITitleView>(view);
            Container.Bind<TitlePresenter>().AsSingle().NonLazy();
        }
    }
}
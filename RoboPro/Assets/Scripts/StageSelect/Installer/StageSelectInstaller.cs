using UnityEngine;
using Zenject;

namespace Robo
{
    public class StageSelectInstaller : MonoInstaller
    {
        [SerializeField] private StageSelectView stageSelectView;

        public override void InstallBindings()
        {
            Container.Bind<IStageSelectModel>().To<StageSelectModel>().AsSingle();
            Container.BindInstance<IStageSelectView>(stageSelectView);
            Container.Bind<StageSelectPresenter>().AsSingle().NonLazy();
        }
    }
}
using Zenject;

namespace Robo
{
    public class MultiSceneManagementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMultiSceneLoader>().To<MultiSceneLoader>().AsSingle();
        }
    }
}
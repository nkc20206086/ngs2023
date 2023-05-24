using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Utility.Installer
{
    public class PostEffectInstaller : MonoInstaller
    {
        [SerializeField] 
        private UniversalRendererData renderData;

        public override void InstallBindings()
        {
            Container.Bind<IPostEffector>().To<PostEffector>().AsCached();
            Container.Bind<UniversalRendererData>().FromInstance(renderData);
        }
    }
}
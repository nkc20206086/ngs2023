using UnityEngine;
using Zenject;

namespace Utility.Installer
{
    [CreateAssetMenu(fileName = "PostEffectMaterialDatabaseInstaller", menuName = "Installers/PostEffectMaterialDatabaseInstaller")]
    public class PostEffectMaterialDatabaseInstaller : ScriptableObjectInstaller<PostEffectMaterialDatabaseInstaller>
    {
        [SerializeField] private PostEffectMaterialDatabase database = null;

        public override void InstallBindings()
        {
            Container.Bind<IPostEffectMaterialDatabase>().FromInstance(database).AsSingle();
        }
    } 
}
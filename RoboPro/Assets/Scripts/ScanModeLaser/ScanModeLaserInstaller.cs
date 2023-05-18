using UnityEngine;
using Zenject;

namespace ScanMode
{
    public class ScanModeLaserInstaller : MonoInstaller
    {
        [SerializeField]
        private ScanModeLaserManager laserManager;

        public override void InstallBindings()
        {
            Container.BindInstance<IScanModeLaserManageable>(laserManager);
        }
    }
}
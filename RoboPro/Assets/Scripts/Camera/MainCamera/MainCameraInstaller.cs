using UnityEngine;
using Zenject;

namespace MainCamera
{
    public class MainCameraInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraBackGroundChanger cameraBackGroundChanger;

        public override void InstallBindings()
        {
            Container.Bind<ICameraBackGroundChanger>().FromInstance(cameraBackGroundChanger);
        }
    }

}

using UnityEngine;

namespace DeathCamera
{
    public class DeathCameraSetting : MonoBehaviour, IDeathCameraSettable
    {
        [SerializeField, Header("éÄÇÒÇæÇ∆Ç´ÇÃîwåiêF")]
        private Color deathBackgroundColor = Color.red;

        private Camera mainCamera;
        private Camera deathCamera;
        private Color defaultBackgroundColor;

        private void Start()
        {
            deathCamera = GetComponent<Camera>();
            mainCamera = transform.parent.GetComponent<Camera>();
            defaultBackgroundColor = mainCamera.backgroundColor;
            ((IDeathCameraSettable)this).InitDeathCameraSetting();
            ((IDeathCameraSettable)this).DeathCameraEnable(false);
        }

        void IDeathCameraSettable.InitDeathCameraSetting()
        {
            deathCamera.transform.position = mainCamera.transform.position;
            deathCamera.transform.rotation = mainCamera.transform.rotation;

            deathCamera.orthographicSize = mainCamera.orthographicSize;
            deathCamera.nearClipPlane = mainCamera.nearClipPlane;
            deathCamera.farClipPlane = mainCamera.farClipPlane;
        }

        void IDeathCameraSettable.DeathCameraEnable(bool enable)
        {
            deathCamera.enabled = enable;
        }

        void IDeathCameraSettable.DrawingByDeathCamera(SkinnedMeshRenderer playerRenderer)
        {
            playerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mainCamera.cullingMask = 0;
            mainCamera.backgroundColor = deathBackgroundColor;
            ((IDeathCameraSettable)this).DeathCameraEnable(true);
        }
        
        void IDeathCameraSettable.StopDrawingByDeathCamera(SkinnedMeshRenderer playerRenderer)
        {
            playerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            mainCamera.cullingMask = -1;
            mainCamera.backgroundColor = defaultBackgroundColor;
            ((IDeathCameraSettable)this).DeathCameraEnable(false);
        }
    }
}

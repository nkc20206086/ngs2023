using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_2 : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float near = 0.3f;

    [SerializeField]
    private float far = 1000f;

    public enum CameraMode
    {
        Ortho,
        Perspective,
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            FadeToOrthoCamera(5, 1, Ease.Linear);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            FadeToPerspectiveCamera(60, 1, Ease.Linear);
        }
    }

    private Matrix4x4 CalcOrthoMatrix(float orthoSize)
    {
        // ïΩçsìäâeÇÃProjectionMatrixÇåvéZÇ∑ÇÈ
        var aspectRatio = (float)Screen.width / Screen.height;
        var orthoWidth = orthoSize * aspectRatio;
        var projMatrix = Matrix4x4.Ortho(orthoWidth * -1, orthoWidth, orthoSize * -1, orthoSize, near, far);
        return projMatrix;
    }

    private Matrix4x4 CalcPerspectiveMatrix(float fov)
    {
        //ìßéãìäâeÇÃProjectionMatrixÇåvéZÇ∑ÇÈ
        var aspectRatio = (float)Screen.width / (float)Screen.height;
        var projMatrix = Matrix4x4.Perspective(fov, aspectRatio, near, far);
        return projMatrix;
    }

    private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        return ret;
    }

    private void FadeToOrthoCamera(float orthoSize, float duration, Ease ease)
    {
        Matrix4x4 nowMatrix = _camera.projectionMatrix;
        Matrix4x4 nextMatrix = CalcOrthoMatrix(orthoSize);
        
        Tween tween = DOVirtual.Float(0, 1, duration, x =>
        {
            _camera.projectionMatrix = MatrixLerp(nowMatrix, nextMatrix, x);
        });
        tween.SetEase(ease);
    }

    private void FadeToPerspectiveCamera(float fov, float duration, Ease ease)
    {
        Matrix4x4 nowMatrix = _camera.projectionMatrix;
        Matrix4x4 nextMatrix = CalcPerspectiveMatrix(fov);

        Tween tween = DOVirtual.Float(0, 1, duration, x =>
        {
            _camera.projectionMatrix = MatrixLerp(nowMatrix, nextMatrix, x);
        });
        tween.SetEase(ease);
    }
}

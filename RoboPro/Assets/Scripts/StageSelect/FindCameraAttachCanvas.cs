using UnityEngine;

public class FindCameraAttachCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
}

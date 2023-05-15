using System;
using UnityEngine;

public class PressCheck : MonoBehaviour
{
    [SerializeField] private float offsetX = 0.1f;
    [SerializeField] private float offsetY = 0.1f;
    [SerializeField] private float offsetZ = 0.1f;

    [SerializeField] private float maxDistance = 1;
    [SerializeField] private float width = 1;
    [SerializeField] private float height = 1;
    [SerializeField] private float length = 1;

    [SerializeField] private LayerMask targetMask;

    //潰された場合発火
    public event Action<Collider, Collider> OnPress;
    //X軸で潰された場合発火
    public event Action<Collider, Collider> OnPressX;
    //Y軸で潰された場合発火
    public event Action<Collider, Collider> OnPressY;
    //Z軸で潰された場合発火
    public event Action<Collider, Collider> OnPressZ;

    private void Update()
    {
        Vector3 pos = transform.position;

        // 上下左右前奥の面にRayを張り、上下、左右、前奥にオブジェクトに接触した場合潰される判定が発火される
        var rayRight1 = new Ray(new Vector3(pos.x + (width / 2 + offsetX), pos.y + (height / 2 + offsetY), pos.z), new Vector3(0, -height, 0));
        var rayRight2 = new Ray(new Vector3(pos.x + (width / 2 + offsetX), pos.y, pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length));

        var rayLeft1 = new Ray(new Vector3(pos.x - (width / 2 + offsetX), pos.y + (height / 2 + offsetY), pos.z), new Vector3(0, -height, 0));
        var rayLeft2 = new Ray(new Vector3(pos.x - (width / 2 + offsetX), pos.y, pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length));

        var rayTop1 = new Ray(new Vector3(pos.x + (width / 2 + offsetX), pos.y + (height / 2 + offsetY), pos.z), new Vector3(-width, 0, 0));
        var rayTop2 = new Ray(new Vector3(pos.x, pos.y + (height / 2 + offsetY), pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length));

        var rayBottom1 = new Ray(new Vector3(pos.x + (width / 2 + offsetX), pos.y - (height / 2 + offsetY), pos.z), new Vector3(-width, 0, 0));
        var rayBottom2 = new Ray(new Vector3(pos.x, pos.y - (height / 2 + offsetY), pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length));

        var rayFront1 = new Ray(new Vector3(pos.x, pos.y + (height / 2 + offsetY), pos.z + (length / 2 + offsetZ)), new Vector3(0, -height, 0));
        var rayFront2 = new Ray(new Vector3(pos.x + (width / 2 + offsetX), pos.y, pos.z + (length / 2 + offsetZ)), new Vector3(-width, 0, 0));

        var rayBack1 = new Ray(new Vector3(pos.x, pos.y + (height / 2 + offsetY), pos.z - (length / 2 + offsetZ)), new Vector3(0, -height, 0));
        var rayBack2 = new Ray(new Vector3(pos.x + (width / 2 + offsetX), pos.y, pos.z - (length / 2 + offsetZ)), new Vector3(-width, 0, 0));

        //デバッグ
        Debug.DrawRay(new Vector3(pos.x + (width / 2 + offsetX), pos.y + (height / 2 + offsetY), pos.z), new Vector3(0, -height, 0), Color.red, 10, false);
        Debug.DrawRay(new Vector3(pos.x + (width / 2 + offsetX), pos.y, pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length), Color.red, 10, false);

        Debug.DrawRay(new Vector3(pos.x - (width / 2 + offsetX), pos.y + (height / 2 + offsetY), pos.z), new Vector3(0, -height, 0), Color.red, 10, false);
        Debug.DrawRay(new Vector3(pos.x - (width / 2 + offsetX), pos.y, pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length), Color.red, 10, false);

        Debug.DrawRay(new Vector3(pos.x + (width / 2 + offsetX), pos.y + (height / 2 + offsetY), pos.z), new Vector3(-width, 0, 0), Color.red, 10, false);
        Debug.DrawRay(new Vector3(pos.x, pos.y + (height / 2 + offsetY), pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length), Color.red, 10, false);

        Debug.DrawRay(new Vector3(pos.x + (width / 2 + offsetX), pos.y - (height / 2 + offsetY), pos.z), new Vector3(-width, 0, 0), Color.red, 10, false);
        Debug.DrawRay(new Vector3(pos.x, pos.y - (height / 2 + offsetY), pos.z + (length / 2 + offsetZ)), new Vector3(0, 0, -length), Color.red, 10, false);

        Debug.DrawRay(new Vector3(pos.x, pos.y + (height / 2 + offsetY), pos.z + (length / 2 + offsetZ)), new Vector3(0, -height, 0), Color.red, 10, false);
        Debug.DrawRay(new Vector3(pos.x + (width / 2 + offsetX), pos.y, pos.z + (length / 2 + offsetZ)), new Vector3(-width, 0, 0), Color.red, 10, false);

        Debug.DrawRay(new Vector3(pos.x, pos.y + (height / 2 + offsetY), pos.z - (length / 2 + offsetZ)), new Vector3(0, -height, 0), Color.red, 10, false);
        Debug.DrawRay(new Vector3(pos.x + (width / 2 + offsetX), pos.y, pos.z - (length / 2 + offsetZ)), new Vector3(-width, 0, 0), Color.red, 10, false);

        Ray[] rays = new Ray[] { rayRight1, rayRight2, rayLeft1, rayLeft2, rayTop1, rayTop2, rayBottom1, rayBottom2, rayFront1, rayFront2, rayBack1, rayBack2 };
        Collider[] hits = new Collider[rays.Length];

        for(int i = 0; i < rays.Length;i++)
        {
            if (Physics.SphereCast(rays[i], 0.01f, out RaycastHit hit, maxDistance, targetMask))
            {
                hits[i] = hit.collider;
                Debug.Log(i);
            }
        }
        
        if((hits[0] != null || hits[1] != null) && (hits[2] != null || hits[3] != null))
        {
            OnPress?.Invoke(hits[0], hits[1]);
            OnPressX?.Invoke(hits[0], hits[1]);
            Debug.Log("X軸ヒット");
        }
        if ((hits[4] != null || hits[5] != null) && (hits[6] != null || hits[7] != null))
        {
            OnPress?.Invoke(hits[2], hits[3]);
            OnPressY?.Invoke(hits[2], hits[3]);
            Debug.Log("Y軸ヒット");
        }
        if ((hits[8] != null || hits[9] != null) && (hits[10] != null || hits[11] != null))
        {
            OnPress?.Invoke(hits[4], hits[5]);
            OnPressZ?.Invoke(hits[4], hits[5]);
            Debug.Log("Z軸ヒット");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private float dir = 1f;
    private Vector3 originVector;
    private bool floorFlg;
    private bool downFloorFlg;
    private bool floatFlg;
    private bool dizzyGroundFlg;
    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float fallRayLength;
    [SerializeField]
    private float landingLength;

    [SerializeField]
    private LayerMask layerMask;

    /// <summary>
    /// ���n���Ă��邩����
    /// </summary>
    /// <returns></returns>
    public bool LandingCheck()
    {
        Vector3 playerPosition = transform.position + new Vector3(0f, 0.1f, 0f);
        RaycastHit ray = new RaycastHit();
        floatFlg = Physics.Raycast(playerPosition, Vector3.down ,out ray,landingLength,layerMask);
        Debug.DrawRay(playerPosition, Vector3.down * landingLength);
        return floatFlg;
    }

    /// <summary>
    /// �ڂ̑O���R������
    /// </summary>
    /// <param name="playerDir"></param>
    /// <returns></returns>
    public bool CheckGround(Vector3 playerDir)
    {
        playerDir = playerDir / 4;
        //��������p���C�̌��_�̌v�Z
        originVector = transform.position + playerDir + new Vector3(0, 0.1f, 0f);
        //���_����90�x�������Ƀ��C���o��
        floorFlg = Physics.Raycast(originVector, -transform.up, rayLength);

        Debug.DrawRay(originVector, -transform.up * rayLength);
        //Debug.Log(floorFlg);
        return floorFlg;
    }

    /// <summary>
    /// ���̃��C���[�𔻒�
    /// </summary>
    /// <returns></returns>
    public bool DizzyGroundFlg()
    {
        Vector3 playerPosition = transform.position + new Vector3(0f, 0.1f, 0f);
        RaycastHit ray = new RaycastHit();
        Physics.Raycast(playerPosition, Vector3.down, out ray, landingLength, layerMask);

        //���C���[�ԍ���8�ԂȂ�
        if (ray.collider == null || ray.collider.gameObject.layer != 8) return true;

        return false;
    }
}

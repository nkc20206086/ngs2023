using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private float dir = 1f;
    private Vector3 originVector;
    private bool floorFlg;
    private bool downFloorFlg;
    [SerializeField]
    private float rayLength;
    [SerializeField]
    private float fallRayLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckGround(Vector3 playerDir)
    {
        Debug.Log(playerDir);
        //落下判定用レイの原点の計算
        originVector = transform.position + playerDir + new Vector3(0, 0.1f, 0);
        //原点から90度下向きにレイを出す
        floorFlg = Physics.Raycast(originVector, -transform.up, rayLength);

        Debug.DrawRay(originVector, -transform.up * rayLength);
        //Debug.Log(floorFlg);
        return floorFlg;
    }
}

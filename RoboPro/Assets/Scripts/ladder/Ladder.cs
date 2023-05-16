using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, ILadderTouchable
{
    [SerializeField]
    float checkRayLength;
    [SerializeField]
    LayerMask checkGroundMask;
    [SerializeField]
    bool debugMode;
    BoxCollider boxCollider;
    bool ILadderTouchable.IsUsable()
    {
        //レイを出す地点を作成
        Vector3 leftUpPos = new Vector3(
            transform.position.x - transform.lossyScale.x * 0.5f * boxCollider.size.x,
            transform.position.y + transform.lossyScale.y * 0.5f * boxCollider.size.y,
            transform.position.z - transform.lossyScale.z * 0.5f * boxCollider.size.z);

        Vector3 rightUpPos = new Vector3(
           transform.position.x + transform.lossyScale.x * 0.5f * boxCollider.size.x,
           transform.position.y + transform.lossyScale.y * 0.5f * boxCollider.size.y,
           transform.position.z - transform.lossyScale.z * 0.5f * boxCollider.size.z);

        Vector3 leftDownPos = new Vector3(
            transform.position.x + transform.lossyScale.x * 0.5f * boxCollider.size.x,
            transform.position.y + transform.lossyScale.y * 0.5f * boxCollider.size.y,
            transform.position.z + transform.lossyScale.z * 0.5f * boxCollider.size.z);

        Vector3 rightDownPos = new Vector3(
            transform.position.x - transform.lossyScale.x * 0.5f * boxCollider.size.x,
            transform.position.y + transform.lossyScale.y * 0.5f * boxCollider.size.y,
            transform.position.z + transform.lossyScale.z * 0.5f * boxCollider.size.z);

        //レイを出してあたり判定を行う
        bool leftUpHit= Physics.Raycast(leftUpPos, transform.up, checkRayLength, checkGroundMask);
        bool rightUpHit = Physics.Raycast(rightUpPos, transform.up, checkRayLength, checkGroundMask);
        bool leftDownHit = Physics.Raycast(leftDownPos, transform.up, checkRayLength, checkGroundMask);
        bool rightDownHit = Physics.Raycast(rightDownPos, transform.up, checkRayLength, checkGroundMask);
        if (debugMode)
        {
            Debug.DrawRay(leftUpPos, transform.up, Color.red, checkRayLength);
            Debug.DrawRay(rightUpPos, transform.up, Color.red, checkRayLength);
            Debug.DrawRay(leftDownPos, transform.up, Color.red, checkRayLength);
            Debug.DrawRay(rightDownPos, transform.up, Color.red, checkRayLength);
        }

        return (leftUpHit || rightUpHit || leftDownHit || rightDownHit);
    }
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
}

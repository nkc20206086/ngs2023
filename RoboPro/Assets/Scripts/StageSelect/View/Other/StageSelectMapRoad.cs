using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectMapRoad : MonoBehaviour
{
    [SerializeField]
    private int length;
    [SerializeField]
    private BlockDB blockDB;
    [SerializeField]
    private int roadBlockPutInterval = 3;
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        for(int i = 0; i < length;i++)
        {
            if(i % roadBlockPutInterval != 0)
            {
                Put(BlockID.GrassBlock, new Vector3(i, 0, 0));
            }
            Put(BlockID.GrassBlock, new Vector3(i, 0, -1));
            Put(BlockID.GrassBlock, new Vector3(i, 0, 1));
            Put(BlockID.DirtBlock, new Vector3(i, -1, 1));
            Put(BlockID.DirtBlock, new Vector3(i, -1, 0));
            Put(BlockID.DirtBlock, new Vector3(i, -1, -1));
        }
    }

    private void Put(BlockID id, Vector3 pos)
    {
        bool isEven = (pos.x + pos.y + pos.z) % 2 == 0;
        GameObject prefab = blockDB.GetData(id).Obj_Odd;
        if (isEven)
        {
            prefab = blockDB.GetData(id).Obj_Even;
        }
        Instantiate(prefab, transform).transform.localPosition = pos + offset;
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockDB", menuName = "RoboPro/BlockDB")]
public class BlockDB : ScriptableObject
{
    [SerializeField] private BlockData[] datas;

    public IReadOnlyList<BlockData> Datas => datas;

    public BlockData GetData(BlockID id)
    {
        foreach (BlockData data in datas)
        {
            if (id == data.ID)
            {
                return data;
            }
        }
        return null;
    }

    public GameObject GetPrefab(BlockID id, int idx)
    {
        BlockData data = GetData(id);
        if (idx % 2 == 0)
        {
            return data.Obj_Odd;
        }
        else
        {
            return data.Obj_Even;
        }
    }
}
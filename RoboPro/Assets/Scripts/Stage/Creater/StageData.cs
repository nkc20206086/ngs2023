using Command;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData
{
    [SerializeField] private BlockDataAllAxis blocks;

    public BlockDataAllAxis Blocks => blocks;

    public List<AccessPointData> AccessPointDatas = new List<AccessPointData>();

    public Vector3 PlayerPosition = new Vector3(0, 1, 0);
    public Vector3 CameraPosition = new Vector3(4, 1, 4);

    public StageData()
    {
        blocks = new BlockDataAllAxis();
    }
}

[Serializable]
public class AccessPointData
{
    public List<CommandContainer> Commands = new List<CommandContainer>();
    public ColorID ColorID;
}

[Serializable]
public class BlockData_XAsis
{
    [SerializeField] private BlockID[] blocks = new BlockID[0];

    public IReadOnlyList<BlockID> Blocks => blocks;

    public void SetBlock(BlockID id, int x)
    {
        if (blocks.Length - 1 < x)
        {
            Array.Resize(ref blocks, x + 1);
        }

        blocks[x] = id;

        for (int i = blocks.Length - 1; i >= x; i--)
        {
            if (blocks[i] == BlockID.Null)
                Array.Resize(ref blocks, i + 1);
            else
                return;
        }
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] != BlockID.Null) return false;
        }
        return true;
    }
}

[Serializable]
public class BlockData_YAsis
{
    [SerializeField] private BlockData_XAsis[] blocks = new BlockData_XAsis[0];

    public IReadOnlyList<BlockData_XAsis> Blocks => blocks;

    public void SetBlock(BlockID id, int x, int y)
    {
        if (blocks.Length - 1 < y)
        {
            Array.Resize(ref blocks, y + 1);
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] == null)
                    blocks[i] = new BlockData_XAsis();
            }
        }

        if (blocks[y] == null)
        {
            blocks[y] = new BlockData_XAsis();
        }
        blocks[y].SetBlock(id, x);

        for (int i = blocks.Length - 1; i >= x; i--)
        {
            if (blocks[i].IsEmpty())
                Array.Resize(ref blocks, i + 1);
            else
                return;
        }
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (!blocks[i].IsEmpty()) return false;
        }
        return true;
    }
}

[System.Serializable]
public class BlockDataAllAxis
{
    [SerializeField] private BlockData_YAsis[] blocks = new BlockData_YAsis[0];

    public IReadOnlyList<BlockData_YAsis> Blocks => blocks;

    public BlockID GetBlock(int x, int y, int z)
    {
        if (blocks == null) return BlockID.Null;
        if (blocks.Length - 1 < z) return BlockID.Null;
        if (blocks[z] == null) return BlockID.Null;
        if (blocks[z].Blocks == null) return BlockID.Null;
        if (blocks[z].Blocks.Count - 1 < y) return BlockID.Null;
        if (blocks[z].Blocks[y] == null) return BlockID.Null;
        if (blocks[z].Blocks[y].Blocks.Count - 1 < x) return BlockID.Null;

        return blocks[z].Blocks[y].Blocks[x];
    }

    public void SetBlock(BlockID id, int x, int y, int z)
    {
        if (blocks.Length - 1 < z)
        {
            Array.Resize(ref blocks, z + 1);
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] == null)
                    blocks[i] = new BlockData_YAsis();
            }
        }

        if (blocks[z] == null)
        {
            blocks[z] = new BlockData_YAsis();
        }
        blocks[z].SetBlock(id, x, y);

        for (int i = blocks.Length - 1; i >= x; i--)
        {
            if (blocks[i].IsEmpty())
                Array.Resize(ref blocks, i + 1);
            else
                return;
        }
    }
}

public enum ColorID
{
    Red,
    Green,
    Blue,
    Purple,
    Yellow,
    Cyan,
    White,
    Black,
}
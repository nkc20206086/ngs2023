using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StagePreview : MonoBehaviour
{
    [SerializeField] private BlockDB blockDB;
    [SerializeField] private Vector3 positionMultiply = new Vector3(0.5f, 0.5f, 0);
    [SerializeField] private float scale = 0.25f;
    [SerializeField] private GameObject[] blindBlocks;
    [SerializeField] private float createBlocktime_1 = 0.5f;
    [SerializeField] private float createBlocktime_2 = 0.5f;
    [SerializeField] private float blindBlockMovePower = 2;

    private List<GameObject> blocks = new List<GameObject>();

    public void ChangeBlind()
    {
        for (int i = 0; i < blocks.Count;i++)
        {
            blindBlocks[i].transform.localScale = blocks[i].transform.localScale;
            blindBlocks[i].SetActive(true);
            Destroy(blocks[i]);
        }
        blocks.Clear();
    }

    public void CreatePreview(StageData data)
    {
        ChangeBlind();

        float xMax = 0;
        float yMax = 0;
        float zMax = data.Blocks.Blocks.Count;
        int blockIndex = 0;

        List<(BlockData, Vector3Int, int)> putList = new List<(BlockData, Vector3Int, int)>();
        for (int z = 0; z < zMax; z++)
        {
            for (int y = 0; y < data.Blocks.Blocks[z].Blocks.Count; y++)
            {
                yMax = Mathf.Max(data.Blocks.Blocks[z].Blocks.Count, yMax);
                for (int x = 0; x < data.Blocks.Blocks[z].Blocks[y].Blocks.Count; x++)
                {
                    xMax = Mathf.Max(data.Blocks.Blocks[z].Blocks[y].Blocks.Count, xMax);

                    BlockID id = data.Blocks.Blocks[z].Blocks[y].Blocks[x];
                    BlockData blockData = blockDB.GetData(id);

                    if (blockData == null) continue;
                    if (blockData.Obj_Odd == null) continue;

                    putList.Add((blockData, new Vector3Int(x, y, z), blockIndex));
                    blockIndex++;
                }
            }
        }


        Vector3 stageSize = new Vector3(xMax * positionMultiply.x, yMax * positionMultiply.y, zMax * positionMultiply.z);
        transform.position -= stageSize / 2;

        Vector3 centerPosition = transform.position;
        for (int i = 0; i < blockIndex; i++)
        {
            PutBlock(putList[i].Item1, putList[i].Item2.x, putList[i].Item2.y, putList[i].Item2.z, putList[i].Item3, centerPosition);
        }
        for(int i = blockIndex; i < blindBlocks.Length;i++)
        {
            blindBlocks[i].SetActive(false);
        }

        DOVirtual.DelayedCall(createBlocktime_1 + createBlocktime_2, () => 
        {
            for (int i = 0; i < blockIndex; i++)
            {
                blindBlocks[i].SetActive(false);
            }
        });
    }

    private void PutBlock(BlockData blockData, int x, int y, int z, int idx, Vector3 centerPos)
    {
        GameObject block;
        if ((z + y + x) % 2 == 0)
        {
            block = Instantiate(blockData.Obj_Even, transform);
        }
        else
        {
            block = Instantiate(blockData.Obj_Odd, transform);
        }
        block.SetActive(false);
        block.transform.localPosition = new Vector3(x, y, z) * scale;
        block.transform.localScale = new Vector3(scale, scale, scale);
        blocks.Add(block);

        Vector3 firstPos = blindBlocks[idx].transform.position;
        Vector3 secondPos = (centerPos - blindBlocks[idx].transform.position).normalized * UnityEngine.Random.Range(0f, blindBlockMovePower);
        Tween tw = DOVirtual.Float(0, 1, createBlocktime_1, j =>
        {
            blindBlocks[idx].transform.position = Vector3.Lerp(firstPos, secondPos, j);
        });
        tw.onComplete += () =>
        {
            Vector3 pos2 = blindBlocks[idx].transform.position;
            Tween tw2 = DOVirtual.Float(0, 1, createBlocktime_2, j =>
            {
                blindBlocks[idx].transform.position = Vector3.Lerp(pos2, block.transform.position, j);
            });

            tw2.onComplete += () =>
            {
                block.SetActive(true);
            };
        };
    }
}

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
    [SerializeField] private GameObject blindBlock;
    [SerializeField] private float createBlocktime_1 = 0.5f;
    [SerializeField] private float createBlocktime_2 = 0.5f;
    [SerializeField] private float blindBlockMovePowerMin = 2;
    [SerializeField] private float blindBlockMovePowerMax = 2;

    private List<GameObject> blocks = new List<GameObject>();
    private List<Tween> tweens = new List<Tween>();

    private Mesh[] meshes = new Mesh[0];
    private Material[] materials;
    private List<Vector3> makedPos = new List<Vector3>();
    private Vector3 makedSize = new Vector3(.5f, .5f, .5f);
    private int instanceNum = 500;
    private List<Matrix4x4> instData = new List<Matrix4x4>();
    private bool isUpdate = false;

    private void Awake()
    {
        InitBlock();
    }

    private void Update()
    {
        if(isUpdate)
        {
            CurrentStageRender();
        }
    }

    /// <summary>
    /// 生成するブロックオブジェクトの情報で初期化
    /// </summary>
    private void InitBlock()
    {
        MeshRenderer[] meshRenderers = blindBlock.GetComponentsInChildren<MeshRenderer>();
        materials = new Material[meshRenderers.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = meshRenderers[i].sharedMaterial;
            materials[i].enableInstancing = true;
        }

        MeshFilter[] meshFilters = blindBlock.GetComponentsInChildren<MeshFilter>();
        meshes = new Mesh[meshFilters.Length];
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i] = meshFilters[i].sharedMesh;
        }
    }

    /// <summary>
    /// ステージを描画する
    /// </summary>
    /// <param name="instanceNum">ブロックの個数</param>
    private void StageRender(int instanceNum)
    {
        for (int i = 0; i < instanceNum; i++)
        {
            instData.Add(Matrix4x4.Translate(makedPos[i]) * Matrix4x4.Scale(makedSize));
        }

        if (instData.Count != 0)
        {
            for (int i = 0; i < meshes.Length; i++)
            {
                var rparams = new RenderParams(materials[i]);
                Graphics.RenderMeshInstanced(rparams, meshes[i], 0, instData);
            }
        }
    }

    /// <summary>
    /// ステージの描画をする
    /// </summary>
    public void CurrentStageRender()
    {
        instData.Clear();
        StageRender(instanceNum);
    }

    public void CreatePreview(StageData data)
    {
        for(int i = 0; i < tweens.Count; i++)
        {
            tweens[i].Kill();
        }
        tweens.Clear();
        for (int i = 0; i < blocks.Count;i++)
        {
            Destroy(blocks[i]);
        }
        blocks.Clear();
        makedPos.Clear();

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
                    makedPos.Add(new Vector3Int(x, y, z));
                    blockIndex++;
                }
            }
        }
        instanceNum = blockIndex;
        isUpdate = true;

        Vector3 stageSize = new Vector3(xMax * positionMultiply.x, yMax * positionMultiply.y, zMax * positionMultiply.z) * scale;
        transform.position -= stageSize / 2;

        Vector3 centerPosition = transform.position;
        for (int i = 0; i < blockIndex; i++)
        {
            PutBlock(putList[i].Item1, putList[i].Item2.x, putList[i].Item2.y, putList[i].Item2.z, putList[i].Item3, centerPosition);
        }

        Tween tw = null;
        tw = DOVirtual.DelayedCall(createBlocktime_1 + createBlocktime_2, () => 
        {
            tweens.Remove(tw);
        });
        tweens.Add(tw);
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
        makedPos[idx] = block.transform.position;

        Vector3 firstPos = makedPos[idx];
        Vector3 secondPos = (centerPos - makedPos[idx]).normalized* UnityEngine.Random.Range(blindBlockMovePowerMin, blindBlockMovePowerMax);
        Tween tw = DOVirtual.Float(0, 1, createBlocktime_1, j =>
        {
            makedPos[idx] = Vector3.Lerp(firstPos, secondPos, j);
        });
        tw.onComplete += () =>
        {
            Vector3 thirdPos = makedPos[idx];
            Tween tw2 = DOVirtual.Float(0, 1, createBlocktime_2, j =>
            {
                makedPos[idx] = Vector3.Lerp(thirdPos, block.transform.position, j);
            });

            tw2.onComplete += () =>
            {
                block.SetActive(true);
                isUpdate = false;
                tweens.Remove(tw);
            };
        };
        tweens.Add(tw);
    }
}

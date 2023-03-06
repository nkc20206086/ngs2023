using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Test_StageSelectedBlockMove : MonoBehaviour
{
    private readonly float blockHighestPos = 10f;
    [SerializeField]
    private GameObject blockObj;

    [SerializeField, Range(0f, 1f)]
    private float stageBreakRatio;

    [SerializeField, Range(0f, 1f)]
    private float stageCreateRatio;


    [SerializeField]
    private Vector2Int stageSize;

    [SerializeField]
    private bool upSet = false;

    private int instanceNum;

    private Vector3[] randomPos;
    private Vector3[] makedPos;
    private int currentCubeNum;
    private float[] heights;
    private Vector2Int[] makedPosRand;
    private Vector3 objSize;
    private List<Matrix4x4> instData = new List<Matrix4x4>();

    private Mesh[] meshes = new Mesh[0];
    private Material[] materials;

    void Start()
    {
        instanceNum = stageSize.x * stageSize.y;

        InitBlock();

        InitGenerate(instanceNum);

        randomPos = new Vector3[instanceNum];

        for (int i = 0; i < instanceNum; i++)
        {
            float randomPosX = (Random.Range(-1f, 1f)) * 10f;
            float randomPosY = (Random.Range(-1f, 1f)) * 10f;
            float randomPosZ = (Random.Range(-1f, 1f)) * 10f;

            randomPos[i] = new Vector3(randomPosX, randomPosY, randomPosZ);
        }
    }

    private void InitBlock()
    {
        MeshRenderer[] meshRenderers = blockObj.GetComponentsInChildren<MeshRenderer>();
        materials = new Material[meshRenderers.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = meshRenderers[i].sharedMaterial;
            materials[i].enableInstancing = true;
        }

        MeshFilter[] meshFilters = blockObj.GetComponentsInChildren<MeshFilter>();
        meshes = new Mesh[meshFilters.Length];
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i] = meshFilters[i].sharedMesh;
        }
    }

    private void InitGenerate(int instanceNum)
    {
        makedPos = new Vector3[instanceNum];
        makedPosRand = new Vector2Int[instanceNum];    // ƒ‰ƒ“ƒ_ƒ€‚É¶¬‚·‚é‡”Ô‚ðŒˆ‚ß‚é   makedPosSeed‚Æ‚©‚É‚µ‚½‚¢‚©‚È
        for (int i = 0; i < stageSize.x; i++)
        {
            for (int j = 0; j < stageSize.y; j++)
            {
                makedPosRand[i * stageSize.x + j] = new Vector2Int(i + 1, j + 1);
            }
        }
        System.Random rand = new System.Random();
        makedPosRand = makedPosRand.OrderBy(x => rand.Next()).ToArray();

        currentCubeNum = 0;
        heights = Enumerable.Repeat(blockHighestPos, instanceNum).ToArray();
        objSize = blockObj.transform.localScale;
    }

    private void StageDestroy(float activateTime)
    {

    }

    private void StageGenerate(float activateTime)
    {

    }



    void Update()
    {
        instData.Clear();
        
        int num = 0;
        for (int i = 1; i <= stageSize.x; i++)
        {
            for (int j = 1; j <= stageSize.y; j++)
            {
                makedPos[num].x = Mathf.Lerp(i - stageSize.x * .5f - .5f, randomPos[num].x, stageBreakRatio);
                makedPos[num].y = Mathf.Lerp(0, randomPos[num].y, stageBreakRatio);
                makedPos[num].z = Mathf.Lerp(j - stageSize.y * .5f - .5f, randomPos[num].z, stageBreakRatio);
                num++;
            }
        }

        objSize.x = Mathf.Lerp(1, 0, stageBreakRatio);
        objSize.y = Mathf.Lerp(1, 0, stageBreakRatio);
        objSize.z = Mathf.Lerp(1, 0, stageBreakRatio);

        if(stageBreakRatio == 1f)
        {
            for (int i = 1; i <= instanceNum; i++)
            {
                makedPos[i - 1].x = makedPosRand[i - 1].x - stageSize.x * .5f - .5f;
                makedPos[i - 1].y = heights[i-1];
                makedPos[i - 1].z = makedPosRand[i - 1].y - stageSize.y * .5f - .5f;

                objSize.x = 1f;
                objSize.y = 1f;
                objSize.z = 1f;

                if (i == instanceNum) upSet = true;
            }

            if (currentCubeNum < instanceNum)
            {
                heights[currentCubeNum] = Mathf.Max(heights[currentCubeNum] - 2f, 0f);
                makedPos[currentCubeNum].y = heights[currentCubeNum];
                if (makedPos[currentCubeNum].y <= 0f)
                {
                    currentCubeNum++;
                }
            }

            //if(upSet)
            //{
            //    
            //}

            objSize.x = 1f;
            objSize.y = 1f;
            objSize.z = 1f;
        }

        for (int i = 0; i < instanceNum; i++)
        {
            instData.Add(Matrix4x4.Translate(makedPos[i]) * Matrix4x4.Scale(objSize));
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

    private void StageBreak()
    {

    }
}

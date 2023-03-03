using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_StageSelectedBlockMove : MonoBehaviour
{
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
    private Vector3 objSize;
    private List<Matrix4x4> instData = new List<Matrix4x4>();

    private Mesh[] meshes = new Mesh[0];
    private Material[] materials;

    void Start()
    {
        instanceNum = stageSize.x * stageSize.y;

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

        makedPos = new Vector3[instanceNum];
        randomPos = new Vector3[instanceNum];
        for (int i = 0; i < instanceNum; i++)
        {
            float randomPosX = (Random.Range(-1f, 1f)) * 10f;
            float randomPosY = (Random.Range(-1f, 1f)) * 10f;
            float randomPosZ = (Random.Range(-1f, 1f)) * 10f;

            randomPos[i] = new Vector3(randomPosX, randomPosY, randomPosZ);
        }

        objSize = blockObj.transform.localScale;
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
                makedPos[num].z = Mathf.Lerp(j - stageSize.x * .5f - .5f, randomPos[num].z, stageBreakRatio);
                num++;
            }
        }

        objSize.x = Mathf.Lerp(1, 0, stageBreakRatio);
        objSize.y = Mathf.Lerp(1, 0, stageBreakRatio);
        objSize.z = Mathf.Lerp(1, 0, stageBreakRatio);

        if(stageBreakRatio == 1f)
        {
            int num2 = 0;
            for (int i = 1; i <= stageSize.x; i++)
            {
                for (int j = 1; j <= stageSize.y; j++)
                {
                    makedPos[num2].x = i - stageSize.x * .5f - .5f;
                    makedPos[num2].y = Mathf.Lerp((num2 + Mathf.Lerp(10f, 0f, stageCreateRatio)), 0f, stageCreateRatio);
                    makedPos[num2].z = j - stageSize.x * .5f - .5f;
                    num2++;
                }
            }

            objSize.x = Mathf.Lerp(0, 1, stageCreateRatio);
            objSize.y = Mathf.Lerp(0, 1, stageCreateRatio);
            objSize.z = Mathf.Lerp(0, 1, stageCreateRatio);
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

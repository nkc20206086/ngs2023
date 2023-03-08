using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.InteropServices;

public class Test_GPGPUStageSelectedBlockMove : MonoBehaviour
{
    private readonly float blockHighestPos = 10f;

    private int instanceNum;

    [SerializeField, Range(0f, 1f)]
    private float stageBreakRatio;

    [SerializeField]
    private ComputeShader computeShader;

    [SerializeField]
    private GameObject blockObj;

    [SerializeField]
    private Vector3Int stageSize;

    [SerializeField]
    private bool upSet = false;

    private int kernelIndex;
    private ComputeBuffer computeBuffer;
    private ComputeBuffer baseHeightBuffer;

    private Vector3[] makedPos;
    private float[] heights;
    private Vector3Int[] makedPosRand;
    private Vector3 objSize;
    private Vector3[] objSizes;
    private List<Matrix4x4> instData = new List<Matrix4x4>();

    private Mesh[] meshes = new Mesh[0];
    private Material[] materials;

    void Start()
    {
        instanceNum = 5 * 5 * 5;

        InitBlock();
        InitGenerate(instanceNum);
        InitComputeShader();
    }

    private void InitComputeShader()
    {
        kernelIndex = computeShader.FindKernel("CSMain");
        computeBuffer = new ComputeBuffer(instanceNum, Marshal.SizeOf<float>());
        computeBuffer.SetData(heights);
        computeShader.SetBuffer(kernelIndex, "cubePosBuffer", computeBuffer);

        baseHeightBuffer = new ComputeBuffer(instanceNum, Marshal.SizeOf<float>());
        baseHeightBuffer.SetData(heights);
        computeShader.SetBuffer(kernelIndex, "baseHeight", baseHeightBuffer);
    }

    /// <summary>
    /// ��������u���b�N�I�u�W�F�N�g�̏��ŏ�����
    /// </summary>
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

    /// <summary>
    /// �����ɕK�v�ȏ���������
    /// </summary>
    /// <param name="instanceNum">�u���b�N�̌�</param>
    private void InitGenerate(int instanceNum)
    {
        makedPos = new Vector3[instanceNum];
        makedPosRand = new Vector3Int[instanceNum];    // �����_���ɐ������鏇�Ԃ����߂�   makedPosSeed�Ƃ��ɂ���������

        Vector2Int[] makedPosXZ = new Vector2Int[stageSize.x * stageSize.z];
        makedPosXZ = MakedPosRandXZ(ref makedPosXZ);

        int num = 0;
        for (int i = 0; i < stageSize.x; i++)
        {
            for (int j = 0; j < stageSize.y; j++)
            {
                for (int k = 0; k < stageSize.z; k++)
                {
                    makedPosRand[num] = new Vector3Int(makedPosXZ[j * stageSize.x + k].x, (stageSize.y - i), makedPosXZ[j * stageSize.x + k].y);
                    num++;
                }
            }
        }

        heights = new float[instanceNum];
        for (int i = 0; i < instanceNum; i++)
        {
            heights[i] = makedPosRand[i].y - stageSize.y * 0.5f + 0.5f;
        }
        objSize = blockObj.transform.localScale;
        objSizes = Enumerable.Repeat(objSize, instanceNum).ToArray();
    }

    private Vector2Int[] MakedPosRandXZ(ref Vector2Int[] array)
    {
        for (int i = 0; i < stageSize.x; i++)
        {
            for (int j = 0; j < stageSize.z; j++)
            {
                array[i * stageSize.x + j] = new Vector2Int(i, j);
            }
        }
        System.Random random = new System.Random();
        array = array.OrderBy(x => random.Next()).ToArray();

        return array;
    }

    /// <summary>
    /// �X�e�[�W��`�悷��
    /// </summary>
    /// <param name="instanceNum">�u���b�N�̌�</param>
    private void StageRender(int instanceNum)
    {
        for (int i = 0; i < instanceNum; i++)
        {
            instData.Add(Matrix4x4.Translate(makedPos[i]) * Matrix4x4.Scale(objSizes[i]));
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
    /// �A�j���[�V���������ŃX�e�[�W�𐶐�
    /// </summary>
    /// <param name="instanceNum">�u���b�N�̌�</param>
    public void StageGenerateNonAnimate(int instanceNum)
    {
        instData.Clear();

        for (int i = 0; i < instanceNum; i++)
        {
            makedPos[i].x = makedPosRand[i].x - stageSize.x * 0.5f + 0.5f;
            makedPos[i].y = makedPosRand[i].y - stageSize.y * 0.5f + 0.5f;
            makedPos[i].z = makedPosRand[i].z - stageSize.z * 0.5f + 0.5f;

            heights[i] = makedPos[i].y;
        }

        StageRender(instanceNum);
    }

    /// <summary>
    /// �X�e�[�W��j�󂷂�
    /// </summary>
    /// <param name="activateTime">�j��ɂ����鎞��</param>
    public void StageDestroy(float activateTime, int instanceNum)
    {
        computeShader.SetInt("instanceNum", instanceNum);
        computeShader.SetFloat("blockHighestPos", blockHighestPos);
        computeShader.SetFloat("stageBreakRatio", stageBreakRatio);

        uint sizeX, sizeY, sizeZ;
        computeShader.GetKernelThreadGroupSizes(kernelIndex, out sizeX, out sizeY, out sizeZ);
        computeShader.Dispatch(kernelIndex, (int)(instanceNum / sizeX), 1, 1);
        var resultData = new float[instanceNum];
        computeBuffer.GetData(resultData);


        instData.Clear();

        for (int i = 0; i < instanceNum; i++)
        {
            float hight = resultData[i];
            float diff = Mathf.Abs(makedPos[i].y - hight);
            makedPos[i].y = hight;

            float size = Mathf.Lerp(1f, 0f, diff / blockHighestPos);
            objSizes[i] = new Vector3(size, size, size);
        }
        StageRender(instanceNum);
    }

    void Update()
    {
        if (upSet)
        {
            StageDestroy(.2f, instanceNum);
        }
        else
        {
            StageGenerateNonAnimate(instanceNum);
        }
    }

    private void OnDestroy()
    {
        computeBuffer.Release();
        baseHeightBuffer.Release();
    }
}

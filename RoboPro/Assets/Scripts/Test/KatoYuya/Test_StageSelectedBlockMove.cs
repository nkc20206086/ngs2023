using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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
    private Vector3Int stageSize;

    [SerializeField]
    private bool upSet = false;

    private int instanceNum;

    private Vector3[] makedPos;
    private float[] heights;
    private Vector3Int[] makedPosRand;
    private Vector3 objSize;
    private Vector3[] objSizes;
    private List<Matrix4x4> instData = new List<Matrix4x4>();

    private Mesh[] meshes = new Mesh[0];
    private Material[] materials;



    private bool destroyed = false;

    void Start()
    {
        //instanceNum = stageSize.x * stageSize.y;

        instanceNum = 5 * 5 * 5;

        InitBlock();
        InitGenerate(instanceNum);

        StageGenerateNonAnimate(5 * 5 * 5);
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
        
        heights = Enumerable.Repeat(blockHighestPos, instanceNum).ToArray();
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
    /// �A�j���[�V��������ŃX�e�[�W�𐶐�
    /// </summary>
    public void StageGenerateAnimate()
    {

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
    /// �X�e�[�W�̕`�������
    /// </summary>
    public void CurrentStageRender()
    {
        instData.Clear();
        StageRender(instanceNum);
    }

    /// <summary>
    /// �X�e�[�W��j�󂷂�
    /// </summary>
    /// <param name="activateTime">�j��ɂ����鎞��</param>
    /// <param name="instanceNum">��</param>
    public void StageDestroy(float activateTime, int instanceNum)
    {
        DOTween.To(() => stageBreakRatio,
                   (x) => stageBreakRatio = x,
                   1f,
                   activateTime).From(0f, true)
               .OnUpdate(() => StageHeightCalc(instanceNum));
    }

    /// <summary>
    /// �X�e�[�W�𐶐�����
    /// </summary>
    /// <param name="activateTime">�����ɂ����鎞��</param>
    /// <param name="instanceNum">��</param>
    public void StageCreate(float activateTime, int instanceNum)
    {
        DOTween.To(() => stageBreakRatio,
                   (x) => stageBreakRatio = x,
                   0f,
                   activateTime).From(1f, true)
               .OnUpdate(() => StageHeightCalc(instanceNum));
    }

    /// <summary>
    /// �X�e�[�W�̍��W���v�Z
    /// </summary>
    /// <param name="instanceNum">��</param>
    private void StageHeightCalc(int instanceNum)
    {
        for (int i = 0; i < instanceNum; i++)
        {
            float hight = Mathf.Clamp(instanceNum * blockHighestPos * (stageBreakRatio - i / (float)instanceNum), heights[i], blockHighestPos);
            float diff = Mathf.Abs(makedPos[i].y - hight);
            makedPos[i].y = hight;

            float size = Mathf.Lerp(1f, 0f, diff / blockHighestPos);
            objSizes[i] = new Vector3(size, size, size);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) StageDestroy(2f, instanceNum);
        if(Input.GetKeyDown(KeyCode.W)) StageCreate(2f, instanceNum);
        CurrentStageRender();
    }
}

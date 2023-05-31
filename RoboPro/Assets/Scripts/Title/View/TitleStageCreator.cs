using System.Collections.Generic;
using UnityEngine;

public class TitleStageCreator : MonoBehaviour
{
    [SerializeField] private TextAsset json;
    [SerializeField] private BlockDB blockDB;
    [SerializeField] private float rotateSpeed = 5;

    private void Start()
    {
        StageData data = JsonUtility.FromJson<StageData>(json.text);
        Load(data);
    }

    private void Load(StageData stageData)
    {
        List<GameObject> objects = new List<GameObject>();
        int maxX = 0;
        int maxY = 0;
        int maxZ = 0;
        
        for (int z = 0; z < stageData.Blocks.Blocks.Count; z++)
        {
            maxZ = Mathf.Max(stageData.Blocks.Blocks.Count, maxZ);
            
            for (int y = 0; y < stageData.Blocks.Blocks[z].Blocks.Count; y++)
            {
                maxY = Mathf.Max(stageData.Blocks.Blocks.Count, maxY);
                
                for (int x = 0; x < stageData.Blocks.Blocks[z].Blocks[y].Blocks.Count; x++)
                {
                    maxX = Mathf.Max(stageData.Blocks.Blocks.Count, maxX);

                    BlockID blockId = stageData.Blocks.Blocks[z].Blocks[y].Blocks[x];
                    GameObject prefabs = blockDB.GetPrefab(blockId, x + y + z);
                    if (prefabs != null)
                    {
                        GameObject instance = Instantiate(prefabs, new Vector3(x, y, z), Quaternion.identity);
                        objects.Add(instance);
                    }
                }
            }
        }

        transform.position = new Vector3(maxX, maxY, maxZ) / 2;
        foreach(GameObject instance in objects)
        {
            instance.transform.SetParent(transform);
        }
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }
}

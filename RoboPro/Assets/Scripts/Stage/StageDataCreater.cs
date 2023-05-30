using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

namespace Stage
{
    public class StageDataCreater: MonoBehaviour 
    {
        [SerializeField]
        private TextAsset stageJson;

        [SerializeField]
        private BlockDB blockDB;

        [SerializeField]
        private GameObject obj;

        public void StageCreate(Dictionary<BlockID, List<GameObject>> dictionary, ref List<AccessPointData> datas)
        {
            string json = null;
            using (StreamReader reader = new StreamReader($"{Application.dataPath}/StageDatas/{stageJson.name}.json"))
            {
                json = reader.ReadToEnd();
            }

            var data = JsonUtility.FromJson<StageData>(json);

            datas = data.AccessPointDatas;

            obj.transform.position = new Vector3(data.Blocks.Blocks[0].Blocks[0].Blocks.Count * 0.5f,data.Blocks.Blocks[0].Blocks.Count * 0.5f,data.Blocks.Blocks.Count * 0.5f);

            for (int z = 0;z < data.Blocks.Blocks.Count; z++)
            {
                for (int y = 0;y < data.Blocks.Blocks[z].Blocks.Count;y++)
                {
                    for (int x = 0;x < data.Blocks.Blocks[z].Blocks[y].Blocks.Count;x++)
                    {
                        BlockID blockId = data.Blocks.Blocks[z].Blocks[y].Blocks[x];
                        GameObject prefabs = blockDB.GetPrefab(blockId, x + y + z);
                        if (prefabs != null)
                        {
                            GameObject instance = Instantiate(prefabs, new Vector3(x, y, z), Quaternion.identity);
                            StageObject obj = instance.AddComponent<StageObject>();
                            obj.Initalize(new Vector3Int(x, y, z));

                            if (blockId >= BlockID.Command_Red)
                            {
                                if (!dictionary.ContainsKey(blockId)) dictionary[blockId] = new List<GameObject>();
                                dictionary[blockId].Add(instance);
                            }
                        }
                    }
                }
            }

            Destroy(this);
        }
    }
}
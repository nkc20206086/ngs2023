using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Robo;

namespace Stage
{
    public class StageDataCreater: MonoBehaviour 
    {
        [Inject] 
        private DiContainer diContainer;
        
        [SerializeField]
        private TextAsset stageJson;

        [SerializeField]
        private BlockDB blockDB;

        [SerializeField]
        private GameObject cameraTarget;

        public void StageCreate(Dictionary<BlockID, List<GameObject>> dictionary, ref List<AccessPointData> datas)
        {
            StageData stageData = default;

            if (stageJson != null)
            {
                string json = null;
                using (StreamReader reader = new StreamReader($"{Application.dataPath}/StageDatas/{stageJson.name}.json"))
                {
                    json = reader.ReadToEnd();
                }

                stageData = JsonUtility.FromJson<StageData>(json);
            }
            else
            {
                stageData = GoToStageArgmentsSingleton.Get().StageData;
            }

            datas = stageData.AccessPointDatas;

            cameraTarget.transform.position = new Vector3(stageData.Blocks.Blocks[0].Blocks[0].Blocks.Count * 0.5f, stageData.Blocks.Blocks[0].Blocks.Count * 0.5f, stageData.Blocks.Blocks.Count * 0.5f);

            for (int z = 0;z < stageData.Blocks.Blocks.Count; z++)
            {
                for (int y = 0;y < stageData.Blocks.Blocks[z].Blocks.Count;y++)
                {
                    for (int x = 0;x < stageData.Blocks.Blocks[z].Blocks[y].Blocks.Count;x++)
                    {
                        BlockID blockId = stageData.Blocks.Blocks[z].Blocks[y].Blocks[x];
                        GameObject prefabs = blockDB.GetPrefab(blockId, x + y + z);
                        if (prefabs != null)
                        {
                            GameObject instance = diContainer.InstantiatePrefab(prefabs, new Vector3(x, y, z), Quaternion.identity, null);

                            if (blockId >= BlockID.Command_Red)
                            {
                                if (!dictionary.ContainsKey(blockId)) dictionary[blockId] = new List<GameObject>();
                                dictionary[blockId].Add(instance);
                            }
                        }
                    }
                }
            }

            for (BlockID id = BlockID.Command_Red; id <= BlockID.Command_Black;id++)
            {
                if (!dictionary.ContainsKey(id)) continue;
                for (int i = 0;i < dictionary[id].Count;i++)
                {
                    Vector3Int[] positions = new Vector3Int[dictionary[id].Count];

                    for (int j = 0;j < dictionary[id].Count; j++)
                    {
                        positions[i] = new Vector3Int((int)dictionary[id][i].transform.position.x,
                                                      (int)dictionary[id][i].transform.position.y,
                                                      (int)dictionary[id][i].transform.position.z);
                    }


                }
            }

            Destroy(this);
        }
        private void Calc(List<GameObject> l)
        {

        }
    }

}
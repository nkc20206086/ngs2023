using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using Zenject;
using Cinemachine;
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

        [SerializeField]
        private GameObject player;

        [SerializeField]
        private CinemachineVirtualCamera goalCamera;

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

            player.transform.position = stageData.PlayerPosition;
            goalCamera.transform.position = stageData.CameraPosition;

            cameraTarget.transform.position = new Vector3(stageData.Blocks.Blocks[0].Blocks[0].Blocks.Count * 0.5f, stageData.Blocks.Blocks[0].Blocks.Count * 0.5f, stageData.Blocks.Blocks.Count * 0.5f);

            GameObject field = new GameObject("FieldObject");

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

                            if (blockId == BlockID.Goal)
                            {
                                goalCamera.LookAt = instance.transform;
                            }
                            else if (blockId >= BlockID.Command_Red)
                            {
                                if (!dictionary.ContainsKey(blockId)) dictionary[blockId] = new List<GameObject>();
                                dictionary[blockId].Add(instance);
                            }
                            else
                            {
                                instance.transform.SetParent(field.transform);
                            }
                        }
                    }
                }
            }

            for (BlockID id = BlockID.Command_Red; id <= BlockID.Command_Black;id++)
            {
                if (!dictionary.ContainsKey(id + 100)) continue;
                for (int i = 0;i < dictionary[id + 100].Count;i++)
                {
                    List<Vector3Int> positions = new List<Vector3Int>();

                    for (int j = 0;j < dictionary[id + 100].Count; j++)
                    {
                        positions.Add(new Vector3Int((int)dictionary[id + 100][j].transform.localPosition.x,
                                                     (int)dictionary[id + 100][j].transform.localPosition.y,
                                                     (int)dictionary[id + 100][j].transform.localPosition.z));
                    }

                    List<int> indexs = new List<int>();

                    Calc(positions,indexs,i);

                    if (indexs.Count > 0)
                    {
                        Vector3 position = Vector3.zero;
                        for (int j = 0; j < indexs.Count; j++)
                        {
                            position += positions[indexs[j]];
                        }

                        GameObject parent = new GameObject("ParentObject");
                        parent.transform.position = position / indexs.Count;
                        Debug.Log($"{id}:{position}");

                        for (int j = 0; j < indexs.Count; j++)
                        {
                            dictionary[id + 100][indexs[j]].transform.SetParent(parent.transform);
                        }

                        foreach (Transform chind in parent.transform)
                        {
                            dictionary[id + 100].Remove(chind.gameObject);
                        }

                        dictionary[id + 100].Add(parent);
                    }
                }
            }

            Destroy(this);
        }
        private void Calc(List<Vector3Int> positions, List<int> indexs,int index)
        {
            for (int i = 0;i < positions.Count;i++)
            {
                if (index == i) continue;
                if (indexs.IndexOf(i) >= 0) continue;
                if ((int)Mathf.Abs(Vector3Int.Distance(positions[index], positions[i])) == 1)
                {
                    indexs.Add(i);
                    Calc(positions,indexs,i);
                }
            }
        }
    }

}
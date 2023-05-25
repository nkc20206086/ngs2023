using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Stage
{
    public class StageDataCreater: MonoBehaviour 
    {
        [SerializeField]
        private TextAsset stageJson;

        [SerializeField]
        private BlockDB blockDB;

        public void StageCreate()
        {
            string json = null;
            using (StreamReader reader = new StreamReader($"{Application.dataPath}/StageDatas/{stageJson.name}.json"))
            {
                json = reader.ReadToEnd();
            }

            var data = JsonUtility.FromJson<StageData>(json);

            int zCount = data.Blocks.Blocks.Count;
            int yCount = data.Blocks.Blocks[0].Blocks.Count;
            int xCount = data.Blocks.Blocks[0].Blocks[0].Blocks.Count;

            for (int z = 0;z < data.Blocks.Blocks.Count; z++)
            {
                for (int y = 0;y < data.Blocks.Blocks[z].Blocks.Count;y++)
                {
                    for (int x = 0;x < data.Blocks.Blocks[z].Blocks[y].Blocks.Count;x++)
                    {
                        Instantiate(blockDB.GetPrefab(data.Blocks.Blocks[z].Blocks[y].Blocks[x], x + y + z), new Vector3(x,y,z), Quaternion.identity);
                    }
                }
            }

            Destroy(this);
        }
    }
}
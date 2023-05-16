using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robo
{
    [Serializable]
    public class StageSelectSaveData
    {
        [SerializeField] 
        private List<StageSelectElementSaveData> saveDatas = new List<StageSelectElementSaveData>();
        [SerializeField]
        private string lastPlayedStage; //最後にプレイしたステージ名

        public IReadOnlyList<StageSelectElementSaveData> SaveDatas => saveDatas;
        public string LastPlayedStage => lastPlayedStage;

        public StageSelectElementSaveData GetSaveData(string stageNumber)
        {
            return saveDatas.Find(x => x.StageNumber == stageNumber);
        }

        public void OnPlayStage(string stageNumber)
        {
            lastPlayedStage = stageNumber;
            if(GetSaveData(stageNumber) == null)
            {
                saveDatas.Add(new StageSelectElementSaveData(stageNumber, false));
            }
        }

        public void OnClearStage(string stageNumber)
        {
            StageSelectElementSaveData element = GetSaveData(stageNumber);
            int index = saveDatas.IndexOf(element);
            saveDatas[index] = new StageSelectElementSaveData(stageNumber, true);
        }
    }
}
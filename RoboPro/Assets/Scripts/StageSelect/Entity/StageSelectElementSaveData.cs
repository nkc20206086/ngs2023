using System;
using UnityEngine;

namespace Robo
{
    [Serializable]
    public class StageSelectElementSaveData
    {
        [SerializeField]
        private string stageNumber = "1-1";
        [SerializeField]
        private bool isClear = false;

        public string StageNumber => stageNumber;
        public bool IsClear => isClear;

        public StageSelectElementSaveData(string stageNumber, bool isClear)
        {
            this.stageNumber = stageNumber;
            this.isClear = isClear;
        }
    }
}
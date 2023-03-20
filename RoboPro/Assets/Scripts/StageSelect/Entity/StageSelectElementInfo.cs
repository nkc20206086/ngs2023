using UnityEngine;

namespace Robo
{
    [System.Serializable]
    public class StageSelectElementInfo
    {
        [SerializeField] 
        private string stageNumber = "1-1";

        [SerializeField] 
        private string stageName = "あいうえお平原";

        [SerializeField] 
        private Sprite stageIcon;

        [SerializeField] 
        private bool isClear = false;

        public string StageNumber => stageNumber;
        public string StageName => stageName;
        public Sprite StageIcon => stageIcon;
        public bool IsClear => isClear;
    }
}

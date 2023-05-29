using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Robo
{
    public class StageSelectElementInfoView : MonoBehaviour, IStageSelectElementInfoView
    {
        [SerializeField] 
        private TMP_Text stageNumber;

        [SerializeField]
        private TMP_Text stageName;

        [SerializeField]
        private Transform stageIconPosition;

        [SerializeField]
        private GameObject clearPanel;

        [Inject]
        private IStageSelectView view;

        public Transform IconParent => stageIconPosition;

        public void OnSelect(int idx)
        {
            var info = view.Infos[idx];
            stageNumber.text = info.StageNumber;
            stageName.text = info.StageName;

            var saveData = view.SaveData.GetSaveData(info.StageNumber);
            if(saveData == null)
            {
                clearPanel.SetActive(false);
            }
            else if (!saveData.IsClear)
            {
                clearPanel.SetActive(false);
            }
            else
            {
                clearPanel.SetActive(true);
            }
        }
    }
}
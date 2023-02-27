using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Robo
{
    public class StageSelectElementInfoView : MonoBehaviour, IStageSelectElementInfoView
    {
        [SerializeField] 
        private Text stageNumber;

        [SerializeField]
        private Text stageName;

        [SerializeField]
        private Transform stageIconPosition;

        [SerializeField]
        private Image clearIcon;

        [SerializeField]
        private Sprite clearIconSprite;

        [SerializeField]
        private Sprite notClearIconSprite;

        [Inject]
        private IStageSelectView view;

        public Transform IconParent => stageIconPosition;

        private void Start()
        {
            view.OnSelect += OnSelect;
        }

        private void OnSelect(int idx)
        {
            var info = view.Infos[idx];
            stageNumber.text = info.StageNumber;
            stageName.text = info.StageName;

            if(info.IsClear)
            {
                clearIcon.sprite = clearIconSprite;
            }
            else
            {
                clearIcon.sprite = notClearIconSprite;
            }
        }
    }
}
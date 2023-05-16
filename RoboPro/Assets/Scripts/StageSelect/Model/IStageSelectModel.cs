using System;

namespace Robo
{
    public interface IStageSelectModel
    {
        event Action<StageSelectModelArgs, StageSelectSaveData> OnInitalize;
        event Action<int> OnSelect;
        event Action<int> OnSelectError;
        event Action OnPlay;
        
        void Initalize(StageSelectModelArgs args);
        void Select(int index);
        void Select(string stageNumber);
        void SelectNext();
        void SelectPrevious();
        void Play();
        void Save();
        void LoadSaveData();
    }
}
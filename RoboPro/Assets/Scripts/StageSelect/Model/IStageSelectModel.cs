using System;

namespace Robo
{
    public interface IStageSelectModel
    {
        event Action<StageSelectModelArgs> OnInitalize;
        event Action<int> OnSelect;
        event Action<int> OnSelectError;
        event Action OnPlay;
        
        void Initalize(StageSelectModelArgs args);
        void Select(int index);
        void SelectNext();
        void SelectPrevious();
        void Play();
    }
}
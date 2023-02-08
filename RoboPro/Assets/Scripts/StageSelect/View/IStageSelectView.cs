using System;

namespace Robo
{
    public interface IStageSelectView 
    {
        event Action OnSelectNextKey;
        event Action OnSelectPreviousKey;
        event Action OnPlay;

        void Initalize(StageSelectModelArgs args);
        void Select(int idx);
        void SelectError(int idx);
    }
}
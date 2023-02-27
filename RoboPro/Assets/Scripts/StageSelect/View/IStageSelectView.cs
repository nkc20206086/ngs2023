using System;
using System.Collections.Generic;

namespace Robo
{
    public interface IStageSelectView
    {
        IReadOnlyList<StageSelectElementInfo> Infos { get; }
        IReadOnlyList<StageSelectElementView> Elements { get; }

        event Action<int> OnSelect;
        event Action<int> OnDeselect;
        event Action OnSelectNextKey;
        event Action OnSelectPreviousKey;
        event Action OnPlay;

        void Initalize(StageSelectModelArgs args);
        void Select(int idx);
        void SelectError(int idx);
    }
}
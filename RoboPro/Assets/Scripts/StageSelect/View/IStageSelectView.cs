using System;
using System.Collections.Generic;

namespace Robo
{
    public interface IStageSelectView
    {
        IReadOnlyList<StageSelectElementInfo> Infos { get; }
        IReadOnlyList<StageSelectElementView> Elements { get; }
        StageSelectSaveData SaveData { get; }

        event Action<int> OnSelect;
        event Action<int> OnDeselect;
        event Action OnSelectNextKey;
        event Action OnSelectPreviousKey;
        event Action OnPlay;
        event Action<string> OnClear;
        event Action OnSave;

        void Initalize(StageSelectModelArgs args, StageSelectSaveData saveData);
        void Select(int idx);
        void SelectError(int idx);
        void Play();
    }
}
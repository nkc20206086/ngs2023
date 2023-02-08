using System;

namespace Robo
{
    public class StageSelectModel : IStageSelectModel
    {
        public event Action<StageSelectModelArgs> OnInitalize;
        public event Action<int> OnSelect;
        public event Action<int> OnSelectError;
        public event Action OnPlay;

        private int stageIndex;
        private int length;

        public void Initalize(StageSelectModelArgs args)
        {
            length = args.StageLength;
            OnInitalize?.Invoke(args);
        }

        public void Select(int index)
        {
            if (index >= length || index < 0)
            {
                OnSelectError?.Invoke(index);
                return;
            }

            stageIndex = index;
            OnSelect?.Invoke(stageIndex);
        }

        public void SelectNext()
        {
            Select(stageIndex + 1);
        }

        public void SelectPrevious()
        {
            Select(stageIndex - 1);
        }

        public void Play()
        {
            UnityEngine.Debug.Log("ステージ"+ stageIndex + "をプレイ");
            OnPlay?.Invoke();
        }
    }
}
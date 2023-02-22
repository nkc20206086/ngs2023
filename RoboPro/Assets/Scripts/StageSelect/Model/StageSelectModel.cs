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

        //初期化
        void IStageSelectModel.Initalize(StageSelectModelArgs args)
        {
            length = args.StageLength;
            OnInitalize?.Invoke(args);
        }

        //ステージを選択s
        void IStageSelectModel.Select(int index)
        {
            if (index >= length || index < 0)
            {
                OnSelectError?.Invoke(index);
                return;
            }

            stageIndex = index;
            OnSelect?.Invoke(stageIndex);
        }

        //次のステージを選択
        void IStageSelectModel.SelectNext()
        {
            ((IStageSelectModel)this).Select(stageIndex + 1);
        }

        //前のステージを選択
        void IStageSelectModel.SelectPrevious()
        {
            ((IStageSelectModel)this).Select(stageIndex - 1);
        }

        //ステージをプレイ
        void IStageSelectModel.Play()
        {
            UnityEngine.Debug.Log("ステージ"+ stageIndex + "をプレイ");
            OnPlay?.Invoke();
        }
    }
}
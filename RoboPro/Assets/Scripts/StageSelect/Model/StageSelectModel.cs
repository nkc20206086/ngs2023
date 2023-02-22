using System;

namespace Robo
{
    public class StageSelectModel : IStageSelectModel
    {
        private event Action<StageSelectModelArgs> OnInitalize;
        private event Action<int> OnSelect;
        private event Action<int> OnSelectError;
        private event Action OnPlay;

        private int stageIndex;
        private int length;

        event Action<StageSelectModelArgs> IStageSelectModel.OnInitalize
        {
            add => OnInitalize += value;
            remove => OnInitalize -= value;
        }

        event Action<int> IStageSelectModel.OnSelect
        {
            add => OnSelect += value;
            remove => OnSelect -= value;
        }

        event Action<int> IStageSelectModel.OnSelectError
        {
            add => OnSelectError += value;
            remove => OnSelectError -= value;
        }

        event Action IStageSelectModel.OnPlay
        {
            add => OnPlay += value;
            remove => OnPlay -= value;
        }

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
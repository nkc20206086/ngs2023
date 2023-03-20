using System;
using System.Collections.Generic;

namespace Robo
{
    public class StageSelectModel : IStageSelectModel
    {
        private event Action<StageSelectModelArgs> OnInitalize;
        private event Action<int> OnSelect;
        private event Action<int> OnCannotSelect;
        private event Action OnPlay;

        private int nowSelectedIndex;
        private List<StageSelectElementInfo> infos = new List<StageSelectElementInfo>();

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
            add => OnCannotSelect += value;
            remove => OnCannotSelect -= value;
        }

        event Action IStageSelectModel.OnPlay
        {
            add => OnPlay += value;
            remove => OnPlay -= value;
        }

        //初期化
        void IStageSelectModel.Initalize(StageSelectModelArgs args)
        {
            infos.AddRange(args.Infos);
            OnInitalize?.Invoke(args);
            ((IStageSelectModel)this).Select(0);
        }

        //ステージを選択s
        void IStageSelectModel.Select(int index)
        {
            if (index >= infos.Count || index < 0)
            {
                OnCannotSelect?.Invoke(index);
                return;
            }

            nowSelectedIndex = index;
            OnSelect?.Invoke(nowSelectedIndex);
        }

        //次のステージを選択
        void IStageSelectModel.SelectNext()
        {

            //現在のステージをクリアしていない場合、次へ進めない
            if (!infos[nowSelectedIndex].IsClear)
            {
                OnCannotSelect?.Invoke(nowSelectedIndex + 1);
                return;
            }
            ((IStageSelectModel)this).Select(nowSelectedIndex + 1);

        }

        //前のステージを選択
        void IStageSelectModel.SelectPrevious()
        {

            ((IStageSelectModel)this).Select(nowSelectedIndex - 1);

        }

        //ステージをプレイ
        void IStageSelectModel.Play()
        {
            UnityEngine.Debug.Log("ステージ"+ nowSelectedIndex + "をプレイ");
            OnPlay?.Invoke();
        }
    }
}
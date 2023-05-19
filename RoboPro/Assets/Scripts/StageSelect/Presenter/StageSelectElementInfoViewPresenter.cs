namespace Robo
{
    public class StageSelectElementInfoViewPresenter
    {
        public StageSelectElementInfoViewPresenter(IStageSelectModel model, IStageSelectElementInfoView view)
        {
            model.OnSelect += view.OnSelect;
        }
    }
}
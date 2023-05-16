namespace Robo
{
    public class StageSelectPresenter
    {
        public StageSelectPresenter(IStageSelectModel model, IStageSelectView view)
        {
            model.OnInitalize += view.Initalize;
            model.OnSelect += view.Select;
            model.OnSelectError += view.SelectError;

            view.OnSelectNextKey += model.SelectNext;
            view.OnSelectPreviousKey += model.SelectPrevious;
            view.OnPlay += model.Play;
            view.OnSave += model.Save;
        }
    }
}
namespace Robo
{
    public class StageSelectPresenter
    {
        public StageSelectPresenter(IStageSelectModel model, IStageSelectView view)
        {
            model.OnInitalize += view.Initalize;
            model.OnSelect += view.Select;
            model.OnSelectError += view.SelectError;
            model.OnPlay += view.Play;

            view.OnSelectNextKey += model.SelectNext;
            view.OnSelectPreviousKey += model.SelectPrevious;
            view.OnPlay += model.Play;
            view.OnClear += model.Clear;
            view.OnSave += model.Save;
        }
    }
}
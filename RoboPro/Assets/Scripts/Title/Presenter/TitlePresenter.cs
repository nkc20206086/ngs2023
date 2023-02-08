namespace Robo
{
    public class TitlePresenter
    {
        public TitlePresenter(ITitleModel model, ITitleView view)
        {
            view.OnClickStartButton += model.Start;
            view.OnClickStartButton += model.ShowSettings;
            view.OnClickStartButton += model.Exit;
        }
    }
}
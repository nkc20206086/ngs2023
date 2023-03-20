namespace Robo
{
    public class TitlePresenter
    {
        public TitlePresenter(ITitleModel model, ITitleView view)
        {
            view.OnClickStartButton += model.Start;
            view.OnClickSettingsButton += model.ShowSettings;
            view.OnClickExitButton += model.Exit;
        }
    }
}
using Zenject;

namespace Robo
{
    public class ScreenSettingsPresenter
    {
        [Inject]
        public ScreenSettingsPresenter(IScreenSettings model, IScreenSettingsView view)
        {
            view.GetResolution += model.GetResolution;

            model.OnSetResolution += view.SetResolution;
            model.OnSetIsFullScreen += view.SetIsFullScreen;
        }
    }
}
using Zenject;

namespace Robo
{
    public class ScreenSettingsEditPresenter
    {
        [Inject]
        public ScreenSettingsEditPresenter(IScreenSettings model, IScreenSettingsEditView editView)
        {
            editView.GetResolutions += model.GetResolutions;

            editView.OnSetScreenResolution += model.SetResolution;
            editView.OnSetIsFullScreen += model.SetIsFullScreen;

            editView.GetSettingsData += model.GetSettingsData;
        }
    }
}
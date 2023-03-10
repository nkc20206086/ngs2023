using Zenject;

namespace Robo
{
    public class ScreenSettingsEditPresenter
    {
        [Inject]
        public ScreenSettingsEditPresenter(ISystemSettingsControllable settings, IScreenSettings model, IScreenSettingsEditView editView)
        {
            editView.GetResolutions += model.GetResolutions;

            editView.OnSetScreenResolution += model.SetResolution;
            editView.OnSetIsFullScreen += model.SetIsFullScreen;

            editView.GetSettingsData += settings.GetData;
        }
    }
}
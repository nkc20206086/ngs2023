using Zenject;

namespace Robo
{
    public class AudioSettingsEditPresenter
    {
        [Inject]
        public AudioSettingsEditPresenter(ISystemSettingsControllable settings, IAudioSettings model, IAudioSettingsEditView editView)
        {
            editView.OnSetMasterVolume += model.SetMasterVolume;
            editView.OnSetBGMVolume += model.SetBGMVolume;
            editView.OnSetSEVolume += model.SetSEVolume;

            editView.GetSettingsData += settings.GetData;
        }
    }
}

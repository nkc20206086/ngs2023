using Zenject;

namespace Robo
{
    public class AudioSettingsPresenter
    {
        [Inject]
        public AudioSettingsPresenter(IAudioSettings model, IAudioSettingsView view)
        {
            model.OnSetMasterVolume += view.SetMasterVolume;
            model.OnSetBGMVolume += view.SetBGMVolume;
            model.OnSetSEVolume += view.SetSEVolume;
        }
    }
}

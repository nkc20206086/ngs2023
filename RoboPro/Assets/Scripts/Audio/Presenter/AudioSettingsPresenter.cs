using Zenject;

namespace Robo
{
    public class AudioSettingsPresenter
    {
        [Inject]
        public AudioSettingsPresenter(IAudioSettings settings, IAudioPlayer player)
        {
            settings.OnSetMasterVolume += player.SetMasterVolume;
            settings.OnSetBGMVolume += player.SetBGMVolume;
            settings.OnSetSEVolume += player.SetSEVolume;

            var settingsData = settings.GetSettingsData();
            player.Initalize(new AudioPlayerData(settingsData.MasterVolume, settingsData.BGMVolume, settingsData.SEVolume));
        }
    }
}

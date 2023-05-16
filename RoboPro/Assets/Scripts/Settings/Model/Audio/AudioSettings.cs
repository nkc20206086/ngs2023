using System;

namespace Robo
{
    public class AudioSettings : IAudioSettings
    {
        public event Func<IGetSettingsData> OnGetSettingsData;
        public event Action<float> OnSetMasterVolume;
        public event Action<float> OnSetBGMVolume;
        public event Action<float> OnSetSEVolume;

        public void SetMasterVolume(float volume)
        {
            OnSetMasterVolume?.Invoke(volume);
        }

        public void SetBGMVolume(float volume)
        {
            OnSetBGMVolume?.Invoke(volume);
        }

        public void SetSEVolume(float volume)
        {
            OnSetSEVolume?.Invoke(volume);
        }

        public IGetSettingsData GetSettingsData()
        {
            return OnGetSettingsData();
        }
    }
}

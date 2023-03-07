using System;

namespace Robo
{
    public class AudioSettings : IAudioSettings
    {
        private event Func<IGetSystemSettingsData> OnGetSettingsData;
        private event Action<float> OnSetMasterVolume;
        private event Action<float> OnSetBGMVolume;
        private event Action<float> OnSetSEVolume;

        event Func<IGetSystemSettingsData> IAudioSettings.OnGetSettingsData
        {
            add => OnGetSettingsData += value;
            remove => OnGetSettingsData -= value;
        }

        event Action<float> IAudioSettings.OnSetMasterVolume
        {
            add => OnSetMasterVolume += value;
            remove => OnSetMasterVolume -= value;
        }

        event Action<float> IAudioSettings.OnSetBGMVolume
        {
            add => OnSetBGMVolume += value;
            remove => OnSetBGMVolume -= value;
        }

        event Action<float> IAudioSettings.OnSetSEVolume
        {
            add => OnSetSEVolume += value;
            remove => OnSetSEVolume -= value;
        }

        void IAudioSettings.SetMasterVolume(float volume)
        {
            OnSetMasterVolume?.Invoke(volume);
        }

        void IAudioSettings.SetBGMVolume(float volume)
        {
            OnSetBGMVolume?.Invoke(volume);
        }

        void IAudioSettings.SetSEVolume(float volume)
        {
            OnSetSEVolume?.Invoke(volume);
        }

        IGetSystemSettingsData IAudioSettings.GetSettingsData()
        {
            return OnGetSettingsData();
        }
    }
}

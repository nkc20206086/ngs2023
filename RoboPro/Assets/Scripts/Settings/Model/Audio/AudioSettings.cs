using System;

namespace Robo
{
    public class AudioSettings : IAudioSettings
    {
        private event Action<float> OnSetMasterVolume;
        private event Action<float> OnSetBGMVolume;
        private event Action<float> OnSetSEVolume;

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
    }
}

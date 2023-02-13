using System;

namespace Robo
{
    public interface IAudioSettingsEditView
    {
        event Func<IGetSettingsData> GetSettingsData;
        event Action<float> OnSetMasterVolume;
        event Action<float> OnSetBGMVolume;
        event Action<float> OnSetSEVolume;
    }
}

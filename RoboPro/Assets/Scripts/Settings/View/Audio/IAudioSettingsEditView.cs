using System;

namespace Robo
{
    public interface IAudioSettingsEditView
    {
        event Func<IGetSystemSettingsData> GetSettingsData;
        event Action<float> OnSetMasterVolume;
        event Action<float> OnSetBGMVolume;
        event Action<float> OnSetSEVolume;
    }
}

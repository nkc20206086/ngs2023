using System;

namespace Robo
{
    public interface IAudioSettings
    {
        event Func<IGetSettingsData> OnGetSettingsData;
        event Action<float> OnSetMasterVolume;
        event Action<float> OnSetBGMVolume;
        event Action<float> OnSetSEVolume;

        /// <summary>設定データを取得</summary>
        IGetSettingsData GetSettingsData();
        /// <summary>マスター音量を設定</summary>
        void SetMasterVolume(float volume);
        /// <summary>BGM音量を設定</summary>
        void SetBGMVolume(float volume);
        /// <summary>SE音量を設定</summary>
        void SetSEVolume(float volume);
    }
}

namespace Robo
{
    public interface IAudioSettingsView
    {
        /// <summary>マスター音量設定</summary>
        void SetMasterVolume(float volume);
        /// <summary>BGM音量設定</summary>
        void SetBGMVolume(float volume);
        /// <summary>SE音量設定</summary>
        void SetSEVolume(float volume);
    }
}

namespace Robo
{
    public interface IGetSystemSettingsData
    {
        float MasterVolume { get; }
        float BGMVolume { get; }
        float SEVolume { get; }
        int ScreenResolutionID { get; }
        bool IsFullScreen { get; }
    }
}
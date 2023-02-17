namespace Robo
{
    public interface IGetSettingsData
    {
        float MasterVolume { get; }
        float BGMVolume { get; }
        float SEVolume { get; }
        int ScreenResolutionID { get; }
        bool IsFullScreen { get; }
    }
}
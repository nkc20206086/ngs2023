namespace Robo
{
    public interface ISetSettingsData
    {
        float MasterVolume { get; set; }
        float BGMVolume { get; set; }
        float SEVolume { get; set; }
        int ScreenResolutionID { get; set; }
        bool IsFullScreen { get; set; }
    }
}

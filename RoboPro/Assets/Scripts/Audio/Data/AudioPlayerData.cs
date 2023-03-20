namespace Robo
{
    public class AudioPlayerData
    {
        public readonly float MasterVolume;
        public readonly float BGMVolume;
        public readonly float SEVolume;

        public AudioPlayerData(float masterVolume, float bgmVolume, float seVolume)
        {
            MasterVolume = masterVolume;
            BGMVolume = bgmVolume;
            SEVolume = seVolume;
        }
    }
}

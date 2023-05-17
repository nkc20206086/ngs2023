namespace Robo
{
    public interface IAudioPlayback
    {
        /// <summary>停止</summary>
        void Stop(bool ignoresReleaseTime);
        /// <summary>ポーズ</summary>
        void Pause(bool ignoresReleaseTime);
        /// <summary>ポーズから復帰し、再開する</summary>
        void Restart();
    }
}
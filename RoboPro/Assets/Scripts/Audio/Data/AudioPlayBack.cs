using CriWare;

namespace Robo
{
    public class AudioPlayback : IAudioPlayback
    {
        private readonly CriAtomExPlayback playBack;

        public AudioPlayback(CriAtomExPlayback playBack)
        {
            this.playBack = playBack;
        }

        //停止
        void IAudioPlayback.Stop(bool ignoresReleaseTime)
        {
            playBack.Stop(ignoresReleaseTime);
        }

        //ポーズ
        void IAudioPlayback.Pause(bool ignoresReleaseTime)
        {
            playBack.Pause(ignoresReleaseTime);
        }

        //ポーズから復帰し、再開する
        void IAudioPlayback.Restart()
        {
            playBack.Resume(CriAtomEx.ResumeMode.AllPlayback);
        }
    }
}

using CriWare;

namespace Robo
{
    public class AudioPlayBack : IAudioPlayBack
    {
        private readonly CriAtomExPlayback playBack;

        public AudioPlayBack(CriAtomExPlayback playBack)
        {
            this.playBack = playBack;
        }

        //停止
        void IAudioPlayBack.Stop(bool ignoresReleaseTime)
        {
            playBack.Stop(ignoresReleaseTime);
        }

        //ポーズ
        void IAudioPlayBack.Pause(bool ignoresReleaseTime)
        {
            playBack.Pause(ignoresReleaseTime);
        }

        //ポーズから復帰し、再開する
        void IAudioPlayBack.Resume()
        {
            playBack.Resume(CriAtomEx.ResumeMode.AllPlayback);
        }
    }
}

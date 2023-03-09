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
        public void Stop(bool ignoresReleaseTime)
        {
            playBack.Stop(ignoresReleaseTime);
        }

        //ポーズ
        public void Pause(bool ignoresReleaseTime)
        {
            playBack.Pause(ignoresReleaseTime);
        }

        //ポーズから復帰し、再開する
        public void Resume()
        {
            playBack.Resume(CriAtomEx.ResumeMode.AllPlayback);
        }
    }
}

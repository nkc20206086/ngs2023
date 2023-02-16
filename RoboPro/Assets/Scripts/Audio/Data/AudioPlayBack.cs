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

        public void Stop(bool ignoresReleaseTime)
        {
            playBack.Stop(ignoresReleaseTime);
        }

        public void Pause(bool ignoresReleaseTime)
        {
            playBack.Pause(ignoresReleaseTime);
        }

        public void Resume()
        {
            playBack.Resume(CriAtomEx.ResumeMode.AllPlayback);
        }
    }
}

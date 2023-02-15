using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Robo
{
    public interface IAudioPlayer
    {
        UniTask LoadSheets(List<CueSheetType> sheetTypes);
        void UnloadSheets(List<CueSheetType> sheetTypes);
        void PlaySE(CueSheetType sheetType, string cueName);
        void PlayBGM(CueSheetType sheetType);
        void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec);
        void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec, bool isCrossFade);
        void StopBGM();
        void SetMasterVolume(float volume);
        void SetBGMVolume(float volume);
        void SetSEVolume(float volume);
        void Initalize(AudioPlayerData audioPlayerData);
    }
}
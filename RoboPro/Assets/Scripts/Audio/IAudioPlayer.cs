using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Robo
{
    public interface IAudioPlayer
    {
        /// <summary>初期化</summary>
        void Initalize(AudioPlayerData audioPlayerData);

        /// <summary>使用するキューシートをロードする</summary>
        UniTask LoadSheets(List<CueSheetType> sheetTypes);

        /// <summary>使用しないキューシートをアンロードする</summary>
        void UnloadSheets(List<CueSheetType> sheetTypes);

        /// <summary>SEを鳴らす</summary>
        IAudioPlayBack PlaySE(CueSheetType sheetType, string cueName);

        /// <summary>BGMを鳴らす</summary>
        void PlayBGM(CueSheetType sheetType);

        /// <summary>BGMを鳴らす</summary>
        void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec);

        /// <summary>BGMを鳴らす</summary>
        void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec, bool isCrossFade);

        /// <summary>BGMを止める</summary>
        void StopBGM();

        /// <summary>マスターボリュームを変更する</summary>
        void SetMasterVolume(float volume);

        /// <summary>BGMボリュームを変更する</summary>
        void SetBGMVolume(float volume);

        /// <summary>SEボリュームを変更する</summary>
        void SetSEVolume(float volume);
    }
}
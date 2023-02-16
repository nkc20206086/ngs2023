using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Robo
{
    public interface IAudioPlayer
    {
        /// <summary>������</summary>
        void Initalize(AudioPlayerData audioPlayerData);

        /// <summary>�g�p����L���[�V�[�g�����[�h����</summary>
        UniTask LoadSheets(List<CueSheetType> sheetTypes);

        /// <summary>�g�p���Ȃ��L���[�V�[�g���A�����[�h����</summary>
        void UnloadSheets(List<CueSheetType> sheetTypes);

        /// <summary>SE��炷</summary>
        IAudioPlayBack PlaySE(CueSheetType sheetType, string cueName);

        /// <summary>BGM��炷</summary>
        void PlayBGM(CueSheetType sheetType);

        /// <summary>BGM��炷</summary>
        void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec);

        /// <summary>BGM��炷</summary>
        void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec, bool isCrossFade);

        /// <summary>BGM���~�߂�</summary>
        void StopBGM();

        /// <summary>�}�X�^�[�{�����[����ύX����</summary>
        void SetMasterVolume(float volume);

        /// <summary>BGM�{�����[����ύX����</summary>
        void SetBGMVolume(float volume);

        /// <summary>SE�{�����[����ύX����</summary>
        void SetSEVolume(float volume);
    }
}
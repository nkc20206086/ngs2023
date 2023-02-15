using Zenject;
using CriWare;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Robo
{
    public class AudioPlayer : IAudioPlayer, IInitializable
    {
        private CriAtomExPlayer bgmPlayer;
        private CriAtomExPlayer sePlayer;
        private AudioPlayerData data;

        //BGM�𗬂��Ă��邩
        private bool nowPlayingBGM = false;
        //BGM�̃��v���C
        private CriAtomExPlayback bgmPlayBack;
        
        private float masterVolume;
        private float bgmVolume;
        private float seVolume;

        private Dictionary<CueSheetType, CriAtomCueSheet> cueSheets = new Dictionary<CueSheetType, CriAtomCueSheet>();

        void IInitializable.Initialize()
        {
            bgmPlayer = new CriAtomExPlayer();
            bgmPlayer.AttachFader();
            sePlayer = new CriAtomExPlayer();

            SetMasterVolume(data.MasterVolume);
            SetSEVolume(data.SEVolume);
            SetBGMVolume(data.BGMVolume);
        }

        //�����ݒ�
        public void Initalize(AudioPlayerData data)
        {
            this.data = data;
        }

        //�L���[�V�[�g��ǂݍ��݁A�����g�p�ł���悤�ɂ���
        public async UniTask LoadSheets(List<CueSheetType> sheetTypes)
        {
            //�ǂݍ��܂�Ă��Ȃ��L���[�V�[�g��ǂݍ���
            foreach(CueSheetType type in sheetTypes)
            {
                string name = type.ToString();
                CriAtomCueSheet sheet = CriAtom.GetCueSheet(name);
                if (sheet == null)
                {
                    sheet = CriAtom.AddCueSheetAsync(name, name + ".acb", "");
                    await UniTask.WaitUntil(() => !sheet.IsLoading);
                }
                cueSheets.Add(type, sheet);
            }
        }

        //�L���[�V�[�g���A�����[�h����
        public void UnloadSheets(List<CueSheetType> sheetTypes)
        {
            //�ǂݍ��܂�Ă���L���[�V�[�g���������
            foreach (CueSheetType type in sheetTypes)
            {
                string name = type.ToString();
                cueSheets.Remove(type);
                if(cueSheets.ContainsKey(type))
                {
                    CriAtom.RemoveCueSheet(name);
                }
            }
        }

        //���ʉ����v���C
        public void PlaySE(CueSheetType sheetType, string cueName)
        {
            if(cueSheets.ContainsKey(sheetType))
            {
                sePlayer.SetCue(cueSheets[sheetType].acb, cueName);
                sePlayer.Start();
            }
        }

        //���y���v���C
        public void PlayBGM(CueSheetType sheetType)
        {
            PlayBGMFade(sheetType, 0, 0);
        }

        //���y���v���C
        public void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec)
        {
            PlayBGMFade(sheetType, fadeInMilliSec, fadeOutMilliSec, false);
        }

        //���y���v���C
        public void PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec, bool isCrossFade)
        {
            if(isCrossFade) 
                bgmPlayer.SetFadeInStartOffset(-fadeInMilliSec);
            bgmPlayer.SetFadeInTime(fadeInMilliSec);
            bgmPlayer.SetFadeOutTime(fadeOutMilliSec);
            bgmPlayer.SetCue(cueSheets[sheetType].acb, sheetType.ToString());
            bgmPlayBack = bgmPlayer.Start();
            nowPlayingBGM = true;
        }

        public void StopBGM()
        {
            if (nowPlayingBGM)
            {
                bgmPlayBack.Stop(false);
                nowPlayingBGM = false;
            }
        }

        public void SetMasterVolume(float volume)
        {
            masterVolume = volume;
            SetBGMVolume(bgmVolume);
            SetSEVolume(seVolume);
        }

        public void SetBGMVolume(float volume)
        {
            bgmVolume = volume;
            CriAtom.SetCategoryVolume("BGM", bgmVolume * masterVolume);
        }

        public void SetSEVolume(float volume)
        {
            seVolume = volume;
            CriAtom.SetCategoryVolume("SE", seVolume * masterVolume);
        }
    }
}
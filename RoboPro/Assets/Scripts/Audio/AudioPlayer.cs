using Zenject;
using CriWare;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Robo
{
    public class AudioPlayer : IAudioPlayer, IInitializable
    {
        private CriAtomExPlayer bgmPlayer;
        private CriAtomExPlayer sePlayer;
        private AudioPlayerData data;

        //BGM�𗬂��Ă��邩
        private bool isPlayingBGM = false;
        //���ݗ����Ă���BGM
        private CueSheetType nowPlayingBGM;

        private float masterVolume;
        private float bgmVolume;
        private float seVolume;

        //�L���[�V�[�g�^�C�v���L���[�V�[�g�ƕR�Â�
        private Dictionary<CueSheetType, CriAtomCueSheet> cueSheets = new Dictionary<CueSheetType, CriAtomCueSheet>();
        //����L���[�V�[�g�̐�
        private Dictionary<CueSheetType, int> cueSheetCounts = new Dictionary<CueSheetType, int>();

        void IInitializable.Initialize()
        {
            bgmPlayer = new CriAtomExPlayer();
            bgmPlayer.AttachFader();
            sePlayer = new CriAtomExPlayer();

            ((IAudioPlayer)this).SetMasterVolume(data.MasterVolume);
            ((IAudioPlayer)this).SetSEVolume(data.SEVolume);
            ((IAudioPlayer)this).SetBGMVolume(data.BGMVolume);
        }

        //�����ݒ�
        public void Initalize(AudioPlayerData data)
        {
            this.data = data;
        }

        //�L���[�V�[�g��ǂݍ��݁A�����g�p�ł���悤�ɂ���
        async UniTask IAudioPlayer.LoadSheets(List<CueSheetType> sheetTypes)
        {
            //�ǂݍ��܂�Ă��Ȃ��L���[�V�[�g��ǂݍ���
            foreach(CueSheetType type in sheetTypes)
            {
                string name = type.ToString();
                CriAtomCueSheet sheet = CriAtom.GetCueSheet(name);
                if(sheet == null)
                {
                    sheet = CriAtom.AddCueSheetAsync(name, name + ".acb", "");
                    await UniTask.WaitUntil(() => !sheet.IsLoading);
                }
                if (cueSheetCounts.ContainsKey(type))
                {
                    cueSheetCounts[type]++;
                }
                else
                {
                    cueSheets.Add(type, sheet);
                    cueSheetCounts.Add(type, 1);
                }
            }
        }

        //�L���[�V�[�g���A�����[�h����
        void IAudioPlayer.UnloadSheets(List<CueSheetType> sheetTypes)
        {
            //�ǂݍ��܂�Ă���L���[�V�[�g���������
            foreach (CueSheetType type in sheetTypes)
            {
                if(cueSheetCounts.ContainsKey(type))
                {
                    cueSheetCounts[type]--;
                    if (cueSheetCounts[type] == 0)
                    {
                        cueSheets.Remove(type);
                        cueSheetCounts.Remove(type);
                        if (type == nowPlayingBGM)
                        {
                            //���ݍĐ�����Ă��鉹�y������ꍇ�A�t�F�[�h�I����ɃL���[�V�[�g���폜����
                            ((IAudioPlayer)this).StopBGM();
                            WaitRemoveCueSheet(type);
                        }
                        else
                        {
                            CriAtom.RemoveCueSheet(type.ToString());
                        }
                    }
                }
            }
        }

        //���y�̃t�F�[�h���I�����Ă���L���[�V�[�g���폜����
        private async void WaitRemoveCueSheet(CueSheetType type)
        {
            await UniTask.WaitUntil(() => !bgmPlayer.IsFading());
            CriAtom.RemoveCueSheet(type.ToString());
        }

        //SE��炷
        IAudioPlayback IAudioPlayer.PlaySE(CueSheetType sheetType, string cueName)
        {
            if(cueSheets.ContainsKey(sheetType))
            {
                sePlayer.SetCue(cueSheets[sheetType].acb, cueName);
                return new AudioPlayback(sePlayer.Start());
            }
            return null;
        }

        //BGM��炷
        void IAudioPlayer.PlayBGM(CueSheetType sheetType)
        {
            ((IAudioPlayer)this).PlayBGMFade(sheetType, 0, 0);
        }

        //BGM��炷
        void IAudioPlayer.PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec)
        {
            ((IAudioPlayer)this).PlayBGMFade(sheetType, fadeInMilliSec, fadeOutMilliSec, false);
        }

        //BGM��炷
        void IAudioPlayer.PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec, bool isCrossFade)
        {
            if(isCrossFade) 
                bgmPlayer.SetFadeInStartOffset(-fadeInMilliSec);
            bgmPlayer.SetFadeInTime(fadeInMilliSec);
            bgmPlayer.SetFadeOutTime(fadeOutMilliSec);

            string cueName = cueSheets[sheetType].acb.GetCueInfoList()[0].name;
            bgmPlayer.SetCue(cueSheets[sheetType].acb, cueName);
            bgmPlayer.Start();
            isPlayingBGM = true;
            nowPlayingBGM = sheetType;
            Debug.Log(cueName);
        }

        //BGM���~�߂�
        void IAudioPlayer.StopBGM()
        {
            if (isPlayingBGM)
            {
                bgmPlayer.Stop(false);
                isPlayingBGM = false;
            }
        }

        //�}�X�^�[�{�����[����ύX����
        void IAudioPlayer.SetMasterVolume(float volume)
        {
            masterVolume = volume;
            ((IAudioPlayer)this).SetBGMVolume(bgmVolume);
            ((IAudioPlayer)this).SetSEVolume(seVolume);
        }

        //BGM�{�����[����ύX����
        void IAudioPlayer.SetBGMVolume(float volume)
        {
            bgmVolume = volume;
            CriAtom.SetCategoryVolume("BGM", bgmVolume * masterVolume);
        }

        //SE�{�����[����ύX����
        void IAudioPlayer.SetSEVolume(float volume)
        {
            seVolume = volume;
            CriAtom.SetCategoryVolume("SE", seVolume * masterVolume);
        }
    }
}
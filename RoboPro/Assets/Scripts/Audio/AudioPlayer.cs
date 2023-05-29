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

        //BGMを流しているか
        private bool isPlayingBGM = false;
        //現在流しているBGM
        private CueSheetType nowPlayingBGM;

        private float masterVolume;
        private float bgmVolume;
        private float seVolume;

        //キューシートタイプをキューシートと紐づけ
        private Dictionary<CueSheetType, CriAtomCueSheet> cueSheets = new Dictionary<CueSheetType, CriAtomCueSheet>();
        //同一キューシートの数
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

        //初期設定
        public void Initalize(AudioPlayerData data)
        {
            this.data = data;
        }

        //キューシートを読み込み、音を使用できるようにする
        async UniTask IAudioPlayer.LoadSheets(List<CueSheetType> sheetTypes)
        {
            //読み込まれていないキューシートを読み込む
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

        //キューシートをアンロードする
        void IAudioPlayer.UnloadSheets(List<CueSheetType> sheetTypes)
        {
            //読み込まれているキューシートを解放する
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
                            //現在再生されている音楽がある場合、フェード終了後にキューシートを削除する
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

        //音楽のフェードが終了してからキューシートを削除する
        private async void WaitRemoveCueSheet(CueSheetType type)
        {
            await UniTask.WaitUntil(() => !bgmPlayer.IsFading());
            CriAtom.RemoveCueSheet(type.ToString());
        }

        //SEを鳴らす
        IAudioPlayback IAudioPlayer.PlaySE(CueSheetType sheetType, string cueName)
        {
            if(cueSheets.ContainsKey(sheetType))
            {
                sePlayer.SetCue(cueSheets[sheetType].acb, cueName);
                return new AudioPlayback(sePlayer.Start());
            }
            return null;
        }

        //BGMを鳴らす
        void IAudioPlayer.PlayBGM(CueSheetType sheetType)
        {
            ((IAudioPlayer)this).PlayBGMFade(sheetType, 0, 0);
        }

        //BGMを鳴らす
        void IAudioPlayer.PlayBGMFade(CueSheetType sheetType, int fadeInMilliSec, int fadeOutMilliSec)
        {
            ((IAudioPlayer)this).PlayBGMFade(sheetType, fadeInMilliSec, fadeOutMilliSec, false);
        }

        //BGMを鳴らす
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

        //BGMを止める
        void IAudioPlayer.StopBGM()
        {
            if (isPlayingBGM)
            {
                bgmPlayer.Stop(false);
                isPlayingBGM = false;
            }
        }

        //マスターボリュームを変更する
        void IAudioPlayer.SetMasterVolume(float volume)
        {
            masterVolume = volume;
            ((IAudioPlayer)this).SetBGMVolume(bgmVolume);
            ((IAudioPlayer)this).SetSEVolume(seVolume);
        }

        //BGMボリュームを変更する
        void IAudioPlayer.SetBGMVolume(float volume)
        {
            bgmVolume = volume;
            CriAtom.SetCategoryVolume("BGM", bgmVolume * masterVolume);
        }

        //SEボリュームを変更する
        void IAudioPlayer.SetSEVolume(float volume)
        {
            seVolume = volume;
            CriAtom.SetCategoryVolume("SE", seVolume * masterVolume);
        }
    }
}
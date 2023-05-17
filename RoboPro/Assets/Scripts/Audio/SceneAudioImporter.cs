using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Robo
{
    //キューシートを読み込んで、シーン上でSEやBGMを鳴らせるようにするクラス
    //逆に、このクラスがないシーンでは音を鳴らすことができない
    public class SceneAudioImporter : MonoBehaviour
    {
        /// <summary>読み込むキューシート一覧</summary>
        [SerializeField]
        private List<CueSheetType> sheetTypes;

        /// <summary>シーンをロードしたときにBGMを鳴らすかどうか</summary>
        [HideInInspector]
        public bool isSceneLoadToPlayBGM = false;

        /// <summary>シーンロードしたときに鳴らすBGM</summary>
        [HideInInspector]
        public CueSheetType sceneLoadToPlayBGM;

        /// <summary>BGMのフェードイン、アウトの時間(ms)</summary>
        [HideInInspector]
        public int bgmFadeMilliSecond = 1000;

        /// <summary>BGMをクロスフェードさせるかどうか</summary>
        [HideInInspector]
        public bool crossFadeBGM = true;

        [Inject]
        private IAudioPlayer audioPlayer;

        private async void Start()
        {
            //シーンが生成された後、キューシートをロードする
            await audioPlayer.LoadSheets(sheetTypes);

            //BGMを鳴らす
            if(isSceneLoadToPlayBGM)
            {
                audioPlayer.PlayBGMFade(sceneLoadToPlayBGM, bgmFadeMilliSecond, bgmFadeMilliSecond, crossFadeBGM);
            }
        }

        private void OnDestroy()
        {
            //シーンがアンロードされるとき、キューシートをアンロードする
            audioPlayer.UnloadSheets(sheetTypes);
        }
    }
}
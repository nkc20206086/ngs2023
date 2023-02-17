using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Robo
{
    public class SceneAudioImporter : MonoBehaviour
    {
        [SerializeField]
        private List<CueSheetType> sheetTypes;

        [HideInInspector]
        public bool isSceneLoadToPlayBGM = false;

        [HideInInspector]
        public CueSheetType sceneLoadToPlayBGM;

        [HideInInspector]
        public int bgmFadeMilliSecond = 1000;

        [HideInInspector]
        public bool crossFadeBGM = true;

        [Inject]
        private IAudioPlayer audioPlayer;

        private async void Start()
        {
            await audioPlayer.LoadSheets(sheetTypes);

            if(isSceneLoadToPlayBGM)
            {
                audioPlayer.PlayBGMFade(sceneLoadToPlayBGM, bgmFadeMilliSecond, bgmFadeMilliSecond, crossFadeBGM);
            }
        }

        private void OnDestroy()
        {
            audioPlayer.UnloadSheets(sheetTypes);
        }
    }
}
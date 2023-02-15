using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Robo
{
    public class SceneAudioImporter : MonoBehaviour
    {
        [SerializeField]
        private List<CueSheetType> sheetTypes;

        [Inject]
        private IAudioPlayer audioPlayer;

        private void Start()
        {
            audioPlayer.LoadSheets(sheetTypes);
        }

        private void OnDestroy()
        {
            audioPlayer.UnloadSheets(sheetTypes);
        }
    }
}
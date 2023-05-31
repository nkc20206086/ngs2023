using Zenject;
using UnityEngine;
using UnityEngine.UI;

namespace Robo
{
    public class CloseSettingsButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [Inject]
        private IAudioPlayer audioPlayer;

        [Inject]
        private IMultiSceneLoader multiSceneLoader;

        private bool unloading = false;

        private void Start()
        {
            button.onClick.AddListener(CloseSettings);
        }

        private void CloseSettings()
        {
            audioPlayer.PlaySE(CueSheetType.System, "SE_System_PlayGimmick");
            //Settingsシーンをアンロード中であれば早期リターン
            if (unloading) return;
            unloading = true;
            multiSceneLoader.UnloadScene(SceneID.Settings);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CloseSettings();
            }
        }
    }
}
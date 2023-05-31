using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Robo
{
    [RequireComponent(typeof(Button))]
    public class SceneLoadButton : MonoBehaviour
    {
        [SerializeField]
        private SceneID sceneId;

        [SerializeField]
        private SceneID[] unloadSceneID;

        [Inject]
        private IAudioPlayer audioPlayer;

        [Inject]
        private IMultiSceneLoader multiSceneLoader;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(LoadScene);
        }

        public async void LoadScene()
        {
            audioPlayer.PlaySE(CueSheetType.System, "SE_System_PlayGimmick");
            //既にシーンが開かれている場合、開けなくする
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == sceneId.ToString()) 
                    return;
            }

            await multiSceneLoader.AddScene(sceneId, true);

            foreach (SceneID id in unloadSceneID)
            {
                multiSceneLoader.UnloadScene(id);
            }
        }
    }
}
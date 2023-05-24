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
        private IMultiSceneLoader multiSceneLoader;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(ShowSettings);
        }

        private async void ShowSettings()
        {
            //���ɐݒ�V�[�����J����Ă���ꍇ�A�J���Ȃ�����
            for(int i = 0; i < SceneManager.sceneCount; i++)
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
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SettingsLoadTest : MonoBehaviour
{
    [Inject]
    private ZenjectSceneLoader zenjectSceneLoader;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = "Settings";
            if (ContainsScene(sceneName))
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
            else
            {
                zenjectSceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
    }
    
    private bool ContainsScene(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
    {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}

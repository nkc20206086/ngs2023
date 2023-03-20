using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayTest : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadSceneAsync("AudioTest", LoadSceneMode.Additive);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadSceneAsync("AudioTest2", LoadSceneMode.Additive);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (IsLoaded("AudioTest")) SceneManager.UnloadSceneAsync("AudioTest");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (IsLoaded("AudioTest2")) SceneManager.UnloadSceneAsync("AudioTest2");
        }
    }

    private bool IsLoaded(string sceneName)
    {
        var sceneCount = SceneManager.sceneCount;

        for (var i = 0; i < sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);

            if (scene.name == sceneName && scene.isLoaded) return true;
        }

        return false;
    }
}

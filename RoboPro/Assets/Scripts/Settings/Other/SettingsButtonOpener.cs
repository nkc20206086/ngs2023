using UnityEngine;

namespace Robo
{
    public class SettingsButtonOpener : MonoBehaviour
    {
        [SerializeField]
        private SceneLoadButton button;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                button.LoadScene();
            }
        }
    }

}
using UnityEngine;
using UnityEngine.UI;

namespace Robo
{
    [RequireComponent(typeof(Button))]
    public class QuitGameButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}
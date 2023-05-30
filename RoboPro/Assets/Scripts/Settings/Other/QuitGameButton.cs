using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Robo
{
    [RequireComponent(typeof(Button))]
    public class QuitGameButton : MonoBehaviour
    {
        [Inject]
        private IAudioPlayer audioPlayer;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            audioPlayer.PlaySE(CueSheetType.System, "SE_System_PlayGimmick");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}
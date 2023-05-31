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
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
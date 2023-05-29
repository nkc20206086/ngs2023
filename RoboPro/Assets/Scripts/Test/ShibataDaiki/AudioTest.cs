using Robo;
using UnityEngine;
using Zenject;

public class AudioTest : MonoBehaviour
{
    [Inject] private IAudioPlayer audioPlayer;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            audioPlayer.PlaySE(CueSheetType.yuboku, "yuboku");
        }
    }
}

using Robo;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioPlayTest : MonoBehaviour
{
    [SerializeField] 
    private List<CueSheetType> sheetTypes = new List<CueSheetType>();
    
    [Inject]
    private IAudioPlayer audioPlayer;

    private void Start()
    {
        audioPlayer.Initalize(new AudioPlayerData(1, 1, 1));
        audioPlayer.LoadSheets(sheetTypes);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioPlayer.PlaySE(CueSheetType.Player, "shutter");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioPlayer.PlaySE(CueSheetType.Menu, "sci_fi");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioPlayer.PlaySE(CueSheetType.Stage, "rex");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioPlayer.PlayBGMFade(CueSheetType.yuboku, 1000, 1000, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audioPlayer.PlayBGMFade(CueSheetType.youkou, 1000, 1000, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            audioPlayer.StopBGM();
        }
    }
}

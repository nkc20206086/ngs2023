using Robo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAudioAnimationKey : MonoBehaviour
{
    [Inject]
    private IAudioPlayer audioPlayer;

    public void Move_SE()
    {
        audioPlayer.PlaySE(CueSheetType.Player, "SE_Player_Walk");
    }

    public void Jump_SE()
    {
        audioPlayer.PlaySE(CueSheetType.Player, "SE_Player_Jump");
    }

    public void Landing_SE()
    {
        audioPlayer.PlaySE(CueSheetType.Player, "SE_Player_Landing");
    }

    public void Access_SE()
    {
        audioPlayer.PlaySE(CueSheetType.Player, "SE_Player_Access");
    }

    public void Goal_Access_SE()
    {
        audioPlayer.PlaySE(CueSheetType.Player, "SE_Player_GoalAccess");
    }

    public void Death_SE()
    {
        audioPlayer.PlaySE(CueSheetType.Player, "SE_Player_Death");
    }

    public void Goal_Staging_SE()
    {
        audioPlayer.PlaySE(CueSheetType.Staging, "SE_Staging_Goal");
    }

    public void Goal_BGM()
    {
        audioPlayer.PlayBGM(CueSheetType.ClearBGM);
    }
}

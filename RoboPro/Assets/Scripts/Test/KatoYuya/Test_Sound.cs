using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robo;
using Zenject;

public class Test_Sound : MonoBehaviour
{
    [Inject]
    private new IAudioPlayer audio;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            audio.PlaySE(CueSheetType.TitleBGM, "BGM_Title_01");
        }
    }
}

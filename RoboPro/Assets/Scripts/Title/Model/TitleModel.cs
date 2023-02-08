using System;
using UnityEngine;

namespace Robo
{
    public class TitleModel : ITitleModel
    {
        public event Action OnStart;
        public event Action OnShowSettings;
        public event Action OnExit;

        //ゲーム開始
        public void Start()
        {
            OnStart?.Invoke();
            Debug.Log("Start");
        }

        //設定画面を開く
        public void ShowSettings()
        {
            OnShowSettings?.Invoke();
            Debug.Log("Show");
        }

        //ゲーム終了
        public void Exit()
        {
            OnExit?.Invoke();
            Debug.Log("Exit");
        }
    }
}
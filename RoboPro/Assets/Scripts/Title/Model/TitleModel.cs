using System;
using UnityEngine;

namespace Robo
{
    public class TitleModel : ITitleModel
    {
        private event Action OnStart;
        private event Action OnShowSettings;
        private event Action OnExit;

        event Action ITitleModel.OnStart
        {
            add => OnStart += value;
            remove => OnStart -= value;
        }

        event Action ITitleModel.OnShowSettings
        {
            add => OnShowSettings += value;
            remove => OnShowSettings -= value;
        }

        event Action ITitleModel.OnExit
        {
            add => OnExit += value;
            remove => OnExit -= value;
        }

        //ゲーム開始
        void ITitleModel.Start()
        {
            OnStart?.Invoke();
            Debug.Log("Start");
        }

        //設定画面を開く
        void ITitleModel.ShowSettings()
        {
            OnShowSettings?.Invoke();
            Debug.Log("Show");
        }

        //ゲーム終了
        void ITitleModel.Exit()
        {
            OnExit?.Invoke();
            Debug.Log("Exit");
        }
    }
}
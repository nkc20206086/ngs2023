using System;
using UnityEngine;

namespace Robo
{
    public class TitleModel : ITitleModel
    {
        public event Action OnStart;
        public event Action OnShowSettings;
        public event Action OnExit;

        public void Start()
        {
            OnStart?.Invoke();
            Debug.Log("Start");
        }

        public void ShowSettings()
        {
            OnShowSettings?.Invoke();
            Debug.Log("Show");
        }

        public void Exit()
        {
            OnExit?.Invoke();
            Debug.Log("Exit");
        }
    }
}
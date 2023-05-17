using System;

namespace Robo
{
    public interface ITitleModel
    {
        event Action OnStart;
        event Action OnShowSettings;
        event Action OnExit;

        void Start();
        void ShowSettings();
        void Exit();
    }
}
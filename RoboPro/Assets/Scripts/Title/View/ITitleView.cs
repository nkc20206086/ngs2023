using System;

namespace Robo
{
    public interface ITitleView
    {
        event Action OnClickStartButton;
        event Action OnClickSettingsButton;
        event Action OnClickExitButton;
    }
}
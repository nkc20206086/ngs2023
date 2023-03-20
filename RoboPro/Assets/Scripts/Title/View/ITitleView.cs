using System;

namespace Robo
{
    public interface ITitleView
    {
        //スタートボタン、設定ボタンが押されたときに呼ばれる
        event Action OnClickStartButton;
        event Action OnClickSettingsButton;
        event Action OnClickExitButton;
    }
}
using System;
using UnityEngine;

namespace Robo
{
    public class ScreenSettingsView : IScreenSettingsView
    {
        public event Func<int, Resolution> GetResolution;

        public void SetResolution(int id)
        {
            Resolution resolution = GetResolution(id);
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetIsFullScreen(bool isFullScreen)
        {
            if(isFullScreen)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }
        }
    }
}

using System;
using UnityEngine;

namespace Robo
{
    public class ScreenSettings : IScreenSettings
    {
        public event Action<int> OnSetResolution;
        public event Action<bool> OnSetIsFullScreen;

        void IScreenSettings.SetResolution(int id)
        {
            OnSetResolution?.Invoke(id);
        }

        void IScreenSettings.SetIsFullScreen(bool isFullScreen)
        {
            OnSetIsFullScreen?.Invoke(isFullScreen);
        }

        Resolution IScreenSettings.GetNowResolution()
        {
            Resolution res = new Resolution();
            res.width = Screen.width;
            res.height = Screen.height;
            return res;
        }

        Resolution IScreenSettings.GetResolution(int id)
        {
            return Screen.resolutions[id];
        }

        Resolution[] IScreenSettings.GetResolutions()
        {
            return Screen.resolutions;
        }
    }
}

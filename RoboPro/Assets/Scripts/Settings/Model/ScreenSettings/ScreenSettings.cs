using System;
using UnityEngine;

namespace Robo
{
    public class ScreenSettings : IScreenSettings
    {
        public event Func<IGetSettingsData> OnGetSettingsData;
        public event Action<int> OnSetResolution;
        public event Action<bool> OnSetIsFullScreen;

        public void SetResolution(int id)
        {
            OnSetResolution?.Invoke(id);
        }

        public void SetIsFullScreen(bool isFullScreen)
        {
            OnSetIsFullScreen?.Invoke(isFullScreen);
        }

        public Resolution GetNowResolution()
        {
            Resolution res = new Resolution();
            res.width = Screen.width;
            res.height = Screen.height;
            return res;
        }

        public Resolution GetResolution(int id)
        {
            return Screen.resolutions[id];
        }

        public Resolution[] GetResolutions()
        {
            return Screen.resolutions;
        }

        public IGetSettingsData GetSettingsData()
        {
            return OnGetSettingsData();
        }
    }
}

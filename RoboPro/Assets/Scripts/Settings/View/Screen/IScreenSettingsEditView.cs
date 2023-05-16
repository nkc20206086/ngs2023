using System;
using UnityEngine;

namespace Robo
{
    public interface IScreenSettingsEditView
    {
        event Func<IGetSettingsData> GetSettingsData;
        event Func<Resolution[]> GetResolutions;
        event Action<bool> OnSetIsFullScreen;
        event Action<int> OnSetScreenResolution;
    }
}
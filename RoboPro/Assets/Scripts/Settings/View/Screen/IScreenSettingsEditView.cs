using System;
using UnityEngine;

namespace Robo
{
    public interface IScreenSettingsEditView
    {
        event Func<IGetSystemSettingsData> GetSettingsData;
        event Func<Resolution[]> GetResolutions;
        event Action<bool> OnSetIsFullScreen;
        event Action<int> OnSetScreenResolution;
    }
}
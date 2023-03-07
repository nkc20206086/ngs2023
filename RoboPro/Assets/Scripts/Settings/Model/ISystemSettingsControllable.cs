using System;

namespace Robo
{
    public interface ISystemSettingsControllable
    {
        event Action<IGetSystemSettingsData> OnLoad;

        void Save();
        void Load();
        IGetSystemSettingsData GetData();
    }
}

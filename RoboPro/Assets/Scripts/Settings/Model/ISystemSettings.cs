using System;

namespace Robo
{
    public interface ISystemSettings
    {
        event Action<IGetSystemSettingsData> OnLoad;

        void Save();
        void Load();
        IGetSystemSettingsData GetData();
    }
}

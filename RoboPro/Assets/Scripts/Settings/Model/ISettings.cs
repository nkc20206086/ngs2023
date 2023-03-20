using System;

namespace Robo
{
    public interface ISettings
    {
        event Action<IGetSettingsData> OnLoad;

        void Save();
        void Load();
        IGetSettingsData GetData();
    }
}

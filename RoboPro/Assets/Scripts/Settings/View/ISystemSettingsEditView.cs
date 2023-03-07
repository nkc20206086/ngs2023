using System;

namespace Robo
{
    public interface ISystemSettingsEditView
    {
        event Action OnSave;
        event Action OnLoad;
    }
}

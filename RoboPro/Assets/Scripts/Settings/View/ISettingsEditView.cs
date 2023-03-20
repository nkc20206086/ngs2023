using System;

namespace Robo
{
    public interface ISettingsEditView
    {
        event Action OnSave;
        event Action OnLoad;
    }
}

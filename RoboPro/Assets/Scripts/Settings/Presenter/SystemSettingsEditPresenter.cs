namespace Robo
{
    public class SystemSettingsEditPresenter 
    {
        public SystemSettingsEditPresenter(ISystemSettingsControllable model, ISystemSettingsEditView editView)
        {
            editView.OnSave += model.Save;
            editView.OnLoad += model.Load;
        }
    }
}

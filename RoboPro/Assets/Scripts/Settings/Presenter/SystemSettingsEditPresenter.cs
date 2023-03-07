namespace Robo
{
    public class SystemSettingsEditPresenter 
    {
        public SystemSettingsEditPresenter(ISystemSettings model, ISystemSettingsEditView editView)
        {
            editView.OnSave += model.Save;
            editView.OnLoad += model.Load;
        }
    }
}

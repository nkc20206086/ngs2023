namespace Robo
{
    public class SettingsEditPresenter 
    {
        public SettingsEditPresenter(ISettings model, ISettingsEditView editView)
        {
            editView.OnSave += model.Save;
            editView.OnLoad += model.Load;
        }
    }
}

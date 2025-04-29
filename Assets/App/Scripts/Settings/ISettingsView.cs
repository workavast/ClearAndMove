namespace App.Settings
{
    public interface ISettingsView
    {
        public void Initialize();

        public void OnEnabledManual();
        public void OnDisabledManual();
    }
}
using System;
using Avastrad.SavingAndLoading;

namespace App.Settings
{
    public static class SettingsSaver
    {
        private static readonly JsonSaveAndLoader JsonSaveAndLoader = new("/Settings.json");

        public static bool Exist() 
            => JsonSaveAndLoader.Exist();

        public static SettingsModel Load()
        {
            if (Exist())
                return JsonSaveAndLoader.Load<SettingsModel>();

            throw new NullReferenceException("Doesnt have save");
        }

        public static void Save(SettingsModel settingsModel)
        {
            JsonSaveAndLoader.Save(settingsModel);
        }
        
        public static void Delete()
        {
            JsonSaveAndLoader.DeleteSave();
        }
    }
}
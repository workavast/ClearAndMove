using App.Settings;
using UnityEditor;

namespace App.Scripts.Settings.Editor
{
    public static class SettingsEditor
    {
        [MenuItem(Consts.AppName +"/Delete settings Save")]
        public static void Delete() => SettingsSaver.Delete();
    }
}
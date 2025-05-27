using UnityEditor;

namespace App.Settings.Editor
{
    public static class EditorGameSettingsSaveDeleter
    {
        [MenuItem(Consts.AppName +"/Delete settings Save")]
        public static void Delete() => SettingsSaver.Delete();
    }
}
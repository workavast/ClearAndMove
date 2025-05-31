using App.Settings;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Settings.Editor
{
    [CustomEditor(typeof(SettingsConfigsRepository))]
    public class EditorSettingsConfigsRepository : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Refresh Configs")) 
                SettingsConfigsRepository.RefreshConfigs();
        }
    }
}

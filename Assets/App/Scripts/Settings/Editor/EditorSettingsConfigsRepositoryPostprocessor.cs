using System.Linq;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Settings.Editor
{
    public class EditorSettingsConfigsRepositoryPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (importedAssets.Any(IsOrWasSettingsConfig))
            {
                Debug.Log($"[{nameof(EditorSettingsConfigsRepositoryPostprocessor)}] Detected change, refreshing configs...");
                global::App.Settings.SettingsConfigsRepository.RefreshConfigs();
            }
            else if (deletedAssets.Any(IsLikelySettingsConfigByPath))
            {
                Debug.Log($"[{nameof(EditorSettingsConfigsRepositoryPostprocessor)}] Detected deletion, checking nulls...");
                global::App.Settings.SettingsConfigsRepository.CheckNulls();
            }
        }

        private static bool IsOrWasSettingsConfig(string assetPath)
        {
            if (!assetPath.EndsWith(".asset"))
                return false;

            var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            return asset != null && asset is global::App.Settings.SettingsConfig;
        }

        private static bool IsLikelySettingsConfigByPath(string assetPath) 
            => assetPath.EndsWith(".asset");//If .asset was deleted, well, lets think that it was SettingsConfig
    }
}
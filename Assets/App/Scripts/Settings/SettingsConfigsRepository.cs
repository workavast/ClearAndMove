using System.Collections.Generic;
using UnityEngine;

namespace App.Settings
{
    [CreateAssetMenu(fileName = nameof(SettingsConfigsRepository),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(SettingsConfigsRepository))]
    public class SettingsConfigsRepository : ScriptableObject
    {
        [SerializeField] private List<SettingsConfig> configs;

        public T GetConfig<T>() where T : SettingsConfig
        {
            var targetType = typeof(T);

            foreach (var config in configs)
                if (config.GetType() == targetType)
                    return config as T;

            throw new KeyNotFoundException($"Cant find setting key-type in dictionary: {targetType}");
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void OnRecompile() => RefreshConfigs();
        
        public static void RefreshConfigs()
        {
            var configsRepositories = Resources.FindObjectsOfTypeAll<SettingsConfigsRepository>();
            var configs = Resources.FindObjectsOfTypeAll<SettingsConfig>();

            foreach (var repository in configsRepositories)
            {
                UnityEditor.EditorUtility.SetDirty(repository);
                repository.configs.Clear();
                foreach (var config in configs)
                {
                    if (!repository.configs.Contains(config))
                        repository.configs.Add(config);
                    else
                        Debug.LogWarning($"Duplicate of the config: [{config.GetType()}]");
                }
            }
        }

        public static void CheckNulls()
        {
            var configsRepositories = Resources.FindObjectsOfTypeAll<SettingsConfigsRepository>();
            foreach (var repository in configsRepositories)
            {
                var hasNull = false;

                for (int i = 0; i < repository.configs.Count; i++)
                {
                    if (repository.configs[i] == null)
                    {
                        hasNull = true;
                        repository.configs.RemoveAt(i);
                        i--;
                    }
                }

                if (hasNull)
                    UnityEditor.EditorUtility.SetDirty(repository);
            }
        }
#endif
    }
}
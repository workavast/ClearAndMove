using System;
using App.Tools.ConfigsRepositories;
using UnityEngine;

namespace App.Settings
{
    [CreateAssetMenu(fileName = nameof(SettingsConfigsRepository),
        menuName = Consts.ConfigsPath + "Settings/" + nameof(SettingsConfigsRepository))]
    public class SettingsConfigsRepository : ConfigsRepository<SettingsConfig>
    {
        public T GetConfig<T>() where T : SettingsConfig
        {
            var targetType = typeof(T);

            foreach (var config in Configs)
                if (config.GetType() == targetType)
                    return config as T;

            throw new NullReferenceException($"Cant find config of type: {targetType}");
        }
    }
}
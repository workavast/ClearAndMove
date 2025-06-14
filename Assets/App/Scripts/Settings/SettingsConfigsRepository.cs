using System;
using App.Utils.ConfigsRepositories;
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

        protected override int Comparison(SettingsConfig a, SettingsConfig b)
        {
            if (a == null)
            {
                if (b == null)
                    return 0;
                else
                    return -1;
            }
            else 
            {
                if (b == null)
                    return 1;
                
                if (a.Priority == b.Priority)
                    return 0;

                if (a.Priority > b.Priority)
                    return -1;
                else
                    return 1;
            }
        }
    }
}
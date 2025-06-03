using System.Collections.Generic;
using UnityEngine;

namespace App.Tools.ConfigsRepositories
{
    public abstract partial class ConfigsRepository<TConfig> : ConfigsRepositoryBase 
        where TConfig : ScriptableObject
    {
        [SerializeField] private ConfigsRepositoryCell<TConfig> configsRepositoryCell = new(true);

        protected List<TConfig> Configs => configsRepositoryCell.Configs;
    }
}
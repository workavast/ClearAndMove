using System.Collections.Generic;
using App.Tools.ConfigsRepositories;
using UnityEngine;

namespace App.Missions
{
    [CreateAssetMenu(fileName = nameof(MissionsConfig), menuName = Consts.AppName + "/Configs/Missions/" + nameof(MissionsConfig))]
    public class MissionsConfig : ConfigsRepository<MissionConfig>
    {
        public IReadOnlyList<MissionConfig> Missions => Configs;

        public int GetIndex(MissionConfig config) 
            => Configs.IndexOf(config);
    }
}
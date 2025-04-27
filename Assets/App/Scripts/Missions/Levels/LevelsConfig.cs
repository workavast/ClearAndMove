using System.Collections.Generic;
using UnityEngine;

namespace App.Missions.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelsConfig), menuName =Consts.AppName + "/Configs/" + nameof(LevelsConfig))]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> levelsConfigs;
        
        public IReadOnlyList<LevelConfig> LevelsConfigs => levelsConfigs;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace App.Missions
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName =Consts.AppName + "/Configs/" + nameof(LevelConfig))]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<NetLevel> levelPrefabs;
        
        public IReadOnlyList<NetLevel> LevelPrefabs => levelPrefabs;
    }
}
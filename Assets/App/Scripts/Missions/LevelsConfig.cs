using System.Collections.Generic;
using UnityEngine;

namespace App.Missions
{
    [CreateAssetMenu]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<NetLevel> levelsPrefabs;
        
        public List<NetLevel> LevelsPrefabs => levelsPrefabs;
    }
}
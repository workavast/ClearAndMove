using System.Collections.Generic;
using App.InstantiateProviding;
using App.Missions.Levels;
using Fusion;
using UnityEngine;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class PrepareGeneration : MissionGenerationState
    {
        private readonly InstantiateProvider _instantiateProvider;
        private readonly LevelsConfig _levelsConfig;
        private readonly NetGenerationModel _netGenerationModel;
        
        public PrepareGeneration(NetworkBehaviour netEntity, LevelsConfig levelsConfig,
            NetGenerationModel netGenerationModel)
            : base(netEntity)
        {
            _levelsConfig = levelsConfig;
            _netGenerationModel = netGenerationModel;
        }

        protected override void OnFixedUpdate()
        {
            _netGenerationModel.missionScheme.Clear();
            _netGenerationModel.missionScheme.Capacity = _levelsConfig.LevelsConfigs.Count;
            foreach (var levelConfig in _levelsConfig.LevelsConfigs)
            {
                var levelPrefabsPool = new List<NetLevel>(levelConfig.LevelPrefabs);
                NetLevel selectedLevelPrefab = null;
                for (int i = 0; i < levelConfig.LevelPrefabs.Count; i++)
                {
                    var index = Random.Range(0, levelPrefabsPool.Count);
                    var levelPrefabVariant = levelPrefabsPool[index];
                    if (!_netGenerationModel.missionScheme.Exists(l => l.name == levelPrefabVariant.name))
                    {
                        selectedLevelPrefab = levelPrefabsPool[index];
                        break;
                    }
                    
                    levelPrefabsPool.RemoveAt(index);
                }

                if (selectedLevelPrefab == null)
                {
                    Debug.LogWarning("Can't find enough unique levels ");
                    var index = Random.Range(0, levelPrefabsPool.Count);
                    selectedLevelPrefab = levelConfig.LevelPrefabs[index];
                }
                
                _netGenerationModel.missionScheme.Add(selectedLevelPrefab);
            }

            TryActivateState<Generation>();
        }
    }
}
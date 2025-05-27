using System.Collections.Generic;
using App.Missions.Levels;
using Fusion;
using UnityEngine;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class PrepareGeneration : MissionGenerationState
    {
        private readonly LevelsConfig _levelsConfig;
        private readonly GenerationModel _generationModel;
        
        public PrepareGeneration(NetworkBehaviour netOwner, LevelsConfig levelsConfig,
            GenerationModel generationModel)
            : base(netOwner)
        {
            _levelsConfig = levelsConfig;
            _generationModel = generationModel;
        }

        protected override void OnFixedUpdate()
        {
            _generationModel.missionScheme.Clear();
            _generationModel.missionScheme.Capacity = _levelsConfig.LevelsConfigs.Count;
            foreach (var levelConfig in _levelsConfig.LevelsConfigs)
            {
                var levelPrefabsPool = new List<NetLevel>(levelConfig.LevelPrefabs);
                NetLevel selectedLevelPrefab = null;
                for (int i = 0; i < levelConfig.LevelPrefabs.Count; i++)
                {
                    var index = Random.Range(0, levelPrefabsPool.Count);
                    var levelPrefabVariant = levelPrefabsPool[index];
                    if (!_generationModel.missionScheme.Exists(l => l.name == levelPrefabVariant.name))
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
                
                _generationModel.missionScheme.Add(selectedLevelPrefab);
            }

            TryActivateState<Generation>();
        }
    }
}
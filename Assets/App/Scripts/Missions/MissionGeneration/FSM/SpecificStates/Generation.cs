using System.Collections.Generic;
using App.Entities.Enemy;
using App.InstantiateProviding;
using App.Missions.MissionEnemiesCounter;
using Fusion;
using UnityEngine;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class Generation : MissionGenerationState
    {
        private readonly InstantiateProvider _instantiateProvider;
        private readonly Transform _levelsParent;
        private readonly LevelsConfig _levelsConfig;
        private readonly Mission _mission;
        private readonly EnemyFactory _enemyFactory;

        public Generation(NetworkBehaviour netEntity, Transform levelsParent, LevelsConfig levelsConfig,
            Mission mission, EnemyFactory enemyFactory)
            : base(netEntity)
        {
            _levelsParent = levelsParent;
            _levelsConfig = levelsConfig;
            _mission = mission;
            _enemyFactory = enemyFactory;
        }

        protected override void OnFixedUpdate()
        {
            NetLevel prevLevel = null;
            var levels = new NetLevel[_levelsConfig.LevelsPrefabs.Count];
            for (var i = 0; i < _levelsConfig.LevelsPrefabs.Count; i++)
            {
                var level =  Runner.Spawn(_levelsConfig.LevelsPrefabs[i], _levelsParent.position + Vector3.left * 50 * i, Quaternion.identity);
                level.SetLevelIndex(i, _levelsConfig.LevelsPrefabs.IsEndIndex(i));
                level.SetStairsState(false);
                level.SetMission(_mission);

                var enemies = new List<NetEnemy>();
                foreach (var enemySpawnPoint in level.EnemySpawnPoints)
                    enemies.Add(_enemyFactory.Create(enemySpawnPoint.transform, 1));
                level.SetEnemies(enemies);

                if (prevLevel != null) 
                    prevLevel.SetMovePoint(level.MoveZone);
                prevLevel = level;
                levels[i] = level;
            }
            
            _mission.SetLevels(levels);

            TryActivateState<GenerationIsOver>();
        }
    }
}
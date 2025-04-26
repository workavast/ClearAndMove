using System.Collections.Generic;
using App.Entities.Enemy;
using App.InstantiateProviding;
using Fusion;
using UnityEngine;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class Generation : MissionGenerationState
    {
        private readonly InstantiateProvider _instantiateProvider;
        private readonly Transform _levelsParent;
        private readonly Mission _mission;
        private readonly EnemyFactory _enemyFactory;
        private readonly NetGenerationModel _netGenerationModel;

        public Generation(NetworkBehaviour netEntity, Transform levelsParent, Mission mission, 
            EnemyFactory enemyFactory, NetGenerationModel netGenerationModel)
            : base(netEntity)
        {
            _levelsParent = levelsParent;
            _mission = mission;
            _enemyFactory = enemyFactory;
            _netGenerationModel = netGenerationModel;
        }

        protected override void OnFixedUpdate()
        {
            var missionScheme = _netGenerationModel.missionScheme;
            
            NetLevel prevLevel = null;
            var levels = new NetLevel[missionScheme.Count];
            for (var i = 0; i < missionScheme.Count; i++)
            {
                var level =  Runner.Spawn(missionScheme[i], _levelsParent.position + Vector3.up * 2.5f * i, Quaternion.identity);
                level.SetLevelIndex(i, missionScheme.IsEndIndex(i));
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
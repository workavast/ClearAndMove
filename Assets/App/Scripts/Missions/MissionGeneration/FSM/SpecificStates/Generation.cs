using System.Collections.Generic;
using App.Entities.Enemy;
using App.Missions.Levels;
using App.Weapons;
using Avastrad.Extensions;
using Fusion;
using UnityEngine;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class Generation : MissionGenerationState
    {
        private readonly Transform _levelsParent;
        private readonly Mission _mission;
        private readonly EnemyFactory _enemyFactory;
        private readonly GenerationModel _generationModel;

        public Generation(NetworkBehaviour netOwner, Transform levelsParent, Mission mission, 
            EnemyFactory enemyFactory, GenerationModel generationModel)
            : base(netOwner)
        {
            _levelsParent = levelsParent;
            _mission = mission;
            _enemyFactory = enemyFactory;
            _generationModel = generationModel;
        }

        protected override void OnFixedUpdate()
        {
            var missionScheme = _generationModel.missionScheme;
            
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
                    enemies.Add(_enemyFactory.Create(enemySpawnPoint.transform, 1, WeaponId.ScarEnemy));
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
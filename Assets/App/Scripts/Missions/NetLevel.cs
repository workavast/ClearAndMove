using System;
using System.Collections.Generic;
using App.Entities.Enemy;
using App.Missions.MissionEnemiesCounter;
using App.StairsZone;
using Avastrad.EventBusFramework;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Missions
{
    public class NetLevel : NetworkBehaviour
    {
        [SerializeField] private NetStairsZone stairsZone;
        [field: SerializeField] public Transform MoveZone { get; private set; }

        [Inject] private readonly IEventBus _eventBus;
        
        private EnemySpawnPoint[] _enemySpawnPoints;

        public int LevelIndex { get; private set; }
        public IEnumerable<EnemySpawnPoint> EnemySpawnPoints => _enemySpawnPoints;

        public bool IsLastLevel { get; private set; }
        public Mission Mission { get; private set; }
        
        private EnemiesCounter _enemiesCounter;

        public event Action OnAllEnemiesIsDead;
        
        private void Awake()
        {
            _enemySpawnPoints = GetComponentsInChildren<EnemySpawnPoint>();

            _enemiesCounter = new EnemiesCounter(_eventBus, this);
            _enemiesCounter.OnAllEnemiesIsDead += () => OnAllEnemiesIsDead?.Invoke();
        }

        public Transform GetMovePoint() 
            => stairsZone.MovePoint;
        public void SetMovePoint(Transform movePoint) 
            => stairsZone.SetMovePoint(movePoint);

        public void SetMission(Mission mission)
        {
            Mission = mission;
            stairsZone.SetMission(Mission);
        }
        
        public void SetStairsState(bool isActive) 
            => stairsZone.SetActivityState(isActive);

        /// <returns>(Dead Enemies Count, Full Enemies Count)</returns>
        public (int, int) GetEnemiesCount()
            => _enemiesCounter.GetEnemiesCount();

        public bool AllEnemiesIsDead() 
            => _enemiesCounter.AllEnemiesIsDead();

        public void SetEnemies(IReadOnlyList<NetEnemy> enemies)
            => _enemiesCounter.SetEnemies(enemies);

        public void SetLevelIndex(int index, bool isLastLevel)
        {
            LevelIndex = index;
            IsLastLevel = isLastLevel;
        }
    }
}
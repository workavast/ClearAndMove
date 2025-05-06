using System;
using System.Collections.Generic;
using System.Linq;
using App.Entities;
using App.Entities.Enemy;
using App.EventBus;
using App.Missions.Levels;
using Avastrad.EventBusFramework;
using UnityEngine;

namespace App.Missions.MissionEnemiesCounter
{
    public class EnemiesCounter : IEventReceiver<OnKill>, IDisposable
    {
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        
        public bool LevelIsCleared  { get; private set; }
        
        private readonly IEventBus _eventBus;
        private readonly NetLevel _level;
        private IReadOnlyList<NetEnemy> _enemies;

        public event Action OnAllEnemiesIsDead;
        
        public EnemiesCounter(IEventBus eventBus, NetLevel level)
        {
            _eventBus = eventBus;
            _level = level;
            
            _eventBus.Subscribe(this);
        }

        public void Dispose() 
            => _eventBus.UnSubscribe(this);

        public void SetEnemies(IReadOnlyList<NetEnemy> enemies)
        {
            Debug.Log($"SetEnemies {enemies == null}");
            _enemies = enemies;
        }

        public void OnEvent(OnKill e)
        {
            CheckEnemies();
        }

        private void CheckEnemies()
        {
            if (AllEnemiesIsDead())
            {
                if (!_level.IsLastLevel)
                    _level.SetStairsState(true);
                
                LevelIsCleared = true;
                _eventBus.UnSubscribe(this);
                OnAllEnemiesIsDead?.Invoke();
            }
        }

        /// <returns>(Dead Enemies Count, Full Enemies Count)</returns>
        public (int, int) GetEnemiesCount()
        {
            var deadEnemiesCount = 0;
            foreach (var enemy in _enemies)
                if (enemy.IsDeadOrKnockout())
                    deadEnemiesCount++;
            
            return (deadEnemiesCount, _enemies.Count());
        }
        
        public bool AllEnemiesIsDead()
        {
            var enemiesCount = GetEnemiesCount();
            return enemiesCount.Item1 >= enemiesCount.Item2;
        }
    }
}
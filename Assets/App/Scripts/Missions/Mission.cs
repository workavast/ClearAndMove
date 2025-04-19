using System;
using System.Collections.Generic;
using App.Entities.Player;
using App.EventBus;
using App.NewDirectory1;
using Avastrad.EventBusFramework;
using UnityEngine;
using Zenject;

namespace App.Missions
{
    public class Mission : MonoBehaviour
    {
        [SerializeField] private NetGameState netGameState;

        [Inject] private PlayersEntitiesRepository _playersEntitiesRepository;
        [Inject] private readonly IEventBus _eventBus;
        
        private NetLevel[] _levels;
        private int _activeLevelIndex;

        public bool IsLastLevel => ActiveLevel().IsLastLevel;

        public void SetLevels(NetLevel[] levels)
            => _levels = levels;

        public NetLevel ActiveLevel() => _levels[_activeLevelIndex];

        public void MoveToTheNextLevel()
        {
            var point = ActiveLevel().GetMovePoint();
            foreach (var player in _playersEntitiesRepository.PlayerEntities)
                player.transform.position = point.position;

            _activeLevelIndex++;
            if (IsLastLevel) 
                ActiveLevel().OnAllEnemiesIsDead += OnLastLevelCleared;

            _eventBus.Invoke(new OnLevelChanged(_activeLevelIndex));
        }

        private void OnLastLevelCleared()
        {
            netGameState.SetGameState(false);
        }
    }
}
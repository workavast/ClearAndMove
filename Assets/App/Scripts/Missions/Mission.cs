using App.Entities.Player;
using App.EventBus;
using App.Missions.Levels;
using Avastrad.EventBusFramework;
using UnityEngine;
using Zenject;

namespace App.Missions
{
    public class Mission : MonoBehaviour
    {
        [SerializeField] private MissionConfig missionConfig;
        [SerializeField] private MissionsConfig missionsConfig;

        [Inject] private readonly PlayersEntitiesRepository _playersEntitiesRepository;
        [Inject] private readonly IEventBus _eventBus;
        
        private NetLevel[] _levels;
        private int _activeLevelIndex;

        public bool IsLastLevel => ActiveLevel().IsLastLevel;
        public bool AllEnemiesIsDead { get; private set; }

        public void SetLevels(NetLevel[] levels)
            => _levels = levels;

        public NetLevel ActiveLevel() => _levels[_activeLevelIndex];

        public void MoveToTheNextLevel()
        {
            var point = ActiveLevel().GetMovePoint();
            foreach (var player in _playersEntitiesRepository.PlayerEntities)
            {
                if (player.IsAlive)
                    player.transform.position = point.position;
            }

            _activeLevelIndex++;
            if (IsLastLevel) 
                ActiveLevel().OnAllEnemiesIsDead += OnLastLevelCleared;

            _eventBus.Invoke(new OnLevelChanged(_activeLevelIndex));
        }

        public int GetMissionIndex() 
            => missionsConfig.GetIndex(missionConfig);
        
        private void OnLastLevelCleared()
        {
            AllEnemiesIsDead = true;
        }
    }
}
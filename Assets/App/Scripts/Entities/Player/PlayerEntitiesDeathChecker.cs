using App.Entities.Player;
using App.EventBus;
using Avastrad.EventBusFramework;
using UnityEngine;
using Zenject;

namespace App.DeathChecking
{
    public class PlayerEntitiesDeathChecker : MonoBehaviour, IEventReceiver<OnPlayerKnockout>, IEventReceiver<OnPlayerDeath>
    {
        [Inject] private readonly PlayersEntitiesRepository _playersEntitiesRepository;
        [Inject] private readonly IEventBus _eventBus;
        
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        public bool AllPlayersUnAlive { get; private set; }

        private void Awake()
        {
            _eventBus.Subscribe<OnPlayerKnockout>(this);
            _eventBus.Subscribe<OnPlayerDeath>(this);
        }

        public void OnEvent(OnPlayerKnockout e)
        {
            var playerEntities = _playersEntitiesRepository.PlayerEntities;

            foreach (var playerEntity in playerEntities)
                if (playerEntity.IsAlive)
                    return;

            AllPlayersUnAlive = true;
        }
        
        public void OnEvent(OnPlayerDeath e)
        {
            var playerEntities = _playersEntitiesRepository.PlayerEntities;

            foreach (var playerEntity in playerEntities)
                if (playerEntity.IsAlive)
                    return;

            AllPlayersUnAlive = true;
        }
    }
}
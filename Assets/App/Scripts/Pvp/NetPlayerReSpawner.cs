using System;
using App.Entities.Player;
using App.Players;
using App.Pvp.Gameplay;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Pvp
{
    public class NetPlayerReSpawner : NetworkBehaviour
    {
        [SerializeField] private float spawnDelay = 5f;
        
        [Networked] private TickTimer SpawnDelay { get; set; }
        [Networked] private bool PlayerIsAlive { get; set; }

        [Inject] private readonly GameplaySessionDataRepository _gameplaySessionDataRepository;

        private PlayerRef PlayerRef => Object.InputAuthority;
        
        private PlayerSpawnPointsProvider _playerSpawnPointsProvider;
        private NetPlayerEntity _netPlayerEntity;
        private PlayerFactory _playerFactory;
        private bool _isInitialized;
        
        public event Action<NetPlayerEntity> OnPlayerSpawned;
        
        public void Initialize(PlayerSpawnPointsProvider playerSpawnPointsProvider, PlayerFactory playerFactory)
        {
            if (_isInitialized)
                return;

            _playerSpawnPointsProvider = playerSpawnPointsProvider;
            _playerFactory = playerFactory;
            _isInitialized = true;
         
            if (HasStateAuthority) 
                SpawnPlayer();
        }
        
        public override void Spawned()
        {
            if (HasStateAuthority && _isInitialized) 
                SpawnPlayer();
        }
        
        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (_netPlayerEntity != null) 
                runner.Despawn(_netPlayerEntity.Object);
        }
        
        public override void FixedUpdateNetwork()
        {
            if (!HasStateAuthority)
                return;

            if (!PlayerIsAlive && SpawnDelay.ExpiredOrNotRunning(Runner)) 
                SpawnPlayer();
        }

        public void OnPlayerDeath()
        {
            if (!HasStateAuthority)
                return;

            PlayerIsAlive = false;
            _netPlayerEntity.OnDeath -= OnPlayerDeath;
            Runner.Despawn(_netPlayerEntity.Object);
            PrepareSpawn();
        }
        
        private void PrepareSpawn() 
            => SpawnDelay = TickTimer.CreateFromSeconds(Runner, spawnDelay);
        
        private void SpawnPlayer()
        {
            var weaponId = _gameplaySessionDataRepository.GetData(PlayerRef).SelectedWeapon;
            var armorLevel = _gameplaySessionDataRepository.GetData(PlayerRef).EquippedArmorLevel;
            
            _netPlayerEntity = _playerFactory.Spawn(Object.InputAuthority, 
                _playerSpawnPointsProvider.GetRandomFreeSpawnPoint(), armorLevel, weaponId);
            _netPlayerEntity.OnDeath += OnPlayerDeath;
            PlayerIsAlive = true;
            OnPlayerSpawned?.Invoke(_netPlayerEntity);
        }
    }
}
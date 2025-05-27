using App.Entities.Player;
using App.Missions.SessionData;
using App.Players;
using App.Players.Repository;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Missions
{
    public class NetPlayerEntitySpawner : NetworkBehaviour
    {
        [SerializeField] private PlayerSpawnPointsProvider playerSpawnPointsProvider;
        [SerializeField] private PlayerFactory playerFactory;
        [SerializeField] private NetPlayersReady playersReady;

        [Inject] private readonly IReadOnlyPlayersRepository _playersRepository;
        [Inject] private readonly MissionSessionDataRepository _missionSessionDataRepository;
        [Inject] private readonly PlayersEntitiesRepository _playersEntitiesRepository;

        private bool _gameIsInitialized;

        public override void Spawned()
        {
            if (!HasStateAuthority)
                return;

            playersReady.OnPlayerIsReady += TrySpawnPlayer;
            _playersRepository.OnPlayerLeft += DespawnPlayerEntity;
            
            if (playersReady.AllPlayersIsReady) 
                SpawnPlayers();
            else
                playersReady.OnAllPlayersIsReady += SpawnPlayers;
        }

        private void TrySpawnPlayer(PlayerRef playerRef)
        {
            if(playersReady.AllPlayersIsReady)
                SpawnPlayer(playerRef);
        }

        private void DespawnPlayerEntity(PlayerRef playerRef)
        {
            if (_playersEntitiesRepository.TryGet(playerRef, out var entity)) 
                Runner.Despawn(entity.Object);
        }
        
        private void SpawnPlayers()
        {
            foreach (var player in _playersRepository.Players) 
                SpawnPlayer(player);
        }
        
        private void SpawnPlayer(PlayerRef playerRef)
        {
            var weaponId = _missionSessionDataRepository.GetData(playerRef).SelectedWeapon;
            var armorLevel = _missionSessionDataRepository.GetData(playerRef).EquippedArmorLevel;
            playerFactory.Spawn(playerRef, playerSpawnPointsProvider.SpawnPoints[playerRef.PlayerId - 1], armorLevel, weaponId);
        }
    }
}
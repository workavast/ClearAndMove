using System;
using App.NetworkRunning;
using Fusion;
using UnityEngine;

namespace App.Entities.Player
{
    public class LocalPlayerProvider
    {
        public bool HasEntity => _netPlayerEntity != null && _netPlayerEntity.IsSpawned;
        public Vector3 Position => _netPlayerEntity.transform.position;
        
        public float MaxHealthPoints => _netPlayerEntity.MaxHealthPoints;
        public float CurrentHealthPoints => _netPlayerEntity.NetHealthPoints;

        public int MaxAmmo => _netPlayerEntity.MaxAmmo;
        public int CurrentAmmo => _netPlayerEntity.CurrentAmmo;

        private readonly PlayersEntitiesRepository _playersEntitiesRepository;
        private readonly NetworkRunnerProvider _networkRunnerProvider;

        private NetPlayerEntity _netPlayerEntity;

        public event Action OnWeaponShot; 
        
        public LocalPlayerProvider(PlayersEntitiesRepository playersEntitiesRepository, NetworkRunnerProvider networkRunnerProvider)
        {
            _playersEntitiesRepository = playersEntitiesRepository;
            _networkRunnerProvider = networkRunnerProvider;

            _playersEntitiesRepository.OnPlayerAdd += PlayerEntityAdded;
            _playersEntitiesRepository.OnPlayerRemove += PlayerEntityRemove;
        }

        private void PlayerEntityAdded(PlayerRef playerRef, NetPlayerEntity netPlayerEntity)
        {
            if (_networkRunnerProvider.GetNetworkRunner().LocalPlayer != playerRef)
                return;

            PlayerEntityRemove(playerRef);

            _netPlayerEntity = netPlayerEntity;
            _netPlayerEntity.OnWeaponShot += WeaponShot;
        }
        
        private void PlayerEntityRemove(PlayerRef playerRef)
        {
            if (_networkRunnerProvider.GetNetworkRunner().LocalPlayer != playerRef)
                return;
            if (_netPlayerEntity == null)
                return;

            _netPlayerEntity.OnWeaponShot -= WeaponShot;
            _netPlayerEntity = null;
        }

        private void WeaponShot() 
            => OnWeaponShot?.Invoke();
    }
}
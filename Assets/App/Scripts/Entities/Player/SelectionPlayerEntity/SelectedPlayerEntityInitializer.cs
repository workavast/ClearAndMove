using App.NetworkRunning;
using Fusion;
using UnityEngine;

namespace App.Entities.Player.SelectionPlayerEntity
{
    public class SelectedPlayerEntityInitializer
    {
        private readonly SelectedPlayerEntityProvider _selectedPlayerEntityProvider;
        private readonly PlayersEntitiesRepository _playersEntitiesRepository;
        private readonly NetworkRunnerProvider _networkRunnerProvider;

        public SelectedPlayerEntityInitializer(SelectedPlayerEntityProvider selectedPlayerEntityProvider, 
            PlayersEntitiesRepository playersEntitiesRepository, NetworkRunnerProvider networkRunnerProvider)
        {
            _selectedPlayerEntityProvider = selectedPlayerEntityProvider;
            _playersEntitiesRepository = playersEntitiesRepository;
            _networkRunnerProvider = networkRunnerProvider;
   
            _playersEntitiesRepository.OnPlayerAdd += PlayerEntityAdded;
            foreach (var entity in _playersEntitiesRepository.PlayerEntities)
                PlayerEntityAdded(entity.PlayerRef, entity);
        }

        private void PlayerEntityAdded(PlayerRef playerRef, NetPlayerEntity netPlayerEntity)
        {
            if (_networkRunnerProvider.GetNetworkRunner().LocalPlayer != playerRef)
                return;

            if (_selectedPlayerEntityProvider.HasEntity)
            {
                _playersEntitiesRepository.OnPlayerAdd -= PlayerEntityAdded;
                return;
            }
            
            _selectedPlayerEntityProvider.SetTargetEntity(netPlayerEntity);
        }
    }
}
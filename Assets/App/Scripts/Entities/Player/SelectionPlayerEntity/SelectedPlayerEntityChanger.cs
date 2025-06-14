using App.NetworkRunning;
using App.Utils;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Entities.Player.SelectionPlayerEntity
{
    public class SelectedPlayerEntityChanger
    {
        [Inject] private readonly SelectedPlayerEntityProvider _selectedPlayerEntityProvider;
        [Inject] private readonly PlayersEntitiesRepository _playersEntitiesRepository;
        [Inject] private readonly NetworkRunnerProvider _networkRunnerProvider;

        public void NextTarget()
        {
            var nextPlayer = GetNextPlayer();
            if (_playersEntitiesRepository.TryGet(nextPlayer, out var target))
                _selectedPlayerEntityProvider.SetTargetEntity(target);
            else
                Debug.LogWarning($"Cant find entity of the player: {nextPlayer}");
        }

        public void PrevTarget()
        {
            var prevPlayer = GetPrevPlayer();
            if (_playersEntitiesRepository.TryGet(prevPlayer, out var target))
                _selectedPlayerEntityProvider.SetTargetEntity(target);
            else
                Debug.LogWarning($"Cant find entity of the player: {prevPlayer}");
        }

        private PlayerRef GetNextPlayer()
        {
            if (_networkRunnerProvider.TryGetNetworkRunner(out var runner))
                return runner.GetNextPlayer(_selectedPlayerEntityProvider.ActiveTarget);
            return default;
        }

        private PlayerRef GetPrevPlayer()
        {
            if (_networkRunnerProvider.TryGetNetworkRunner(out var runner))
                return runner.GetPrevPlayer(_selectedPlayerEntityProvider.ActiveTarget);
            return default;
        }
    }
}
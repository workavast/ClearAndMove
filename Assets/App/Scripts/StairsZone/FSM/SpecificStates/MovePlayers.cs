using App.Entities.Player;
using App.NewDirectory1;
using UnityEngine;

namespace App.StairsZone.FSM.SpecificStates
{
    public class MovePlayers : StairsZoneState
    {
        private readonly Transform _movePoint;
        
        public MovePlayers(NetStairsZone netStairsZone, StairsZoneConfig config, NetGameState netGameState,
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView, Transform movePoint) 
            : base(netStairsZone, config, netGameState, playersEntitiesRepository, stairsZoneView)
        {
            _movePoint = movePoint;
        }

        protected override void OnEnterState()
        {
            foreach (var player in PlayersEntitiesRepository.PlayerEntities)
                player.transform.position = _movePoint.position;
        }

        protected override void OnFixedUpdate()
        {
            TryActivateState<Idle>();
        }
    }
}
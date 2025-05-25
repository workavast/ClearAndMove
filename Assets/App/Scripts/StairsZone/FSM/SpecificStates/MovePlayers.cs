using App.Entities.Player;
using UnityEngine;

namespace App.StairsZone.FSM.SpecificStates
{
    public class MovePlayers : StairsZoneState
    {
        public MovePlayers(NetStairsZone netStairsZone, StairsZoneConfig config, 
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView, Collider zoneCollider) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView, zoneCollider) { }

        protected override void OnEnterState()
        {
            NetStairsZone.Mission.MoveToTheNextLevel();
        }

        protected override void OnFixedUpdate()
        {
            TryActivateState<Idle>();
        }
    }
}
using App.Entities.Player;
using UnityEngine;

namespace App.StairsZone.FSM.SpecificStates
{
    public class UnActive : StairsZoneState
    {
        public UnActive(NetStairsZone netStairsZone, StairsZoneConfig config, 
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView, Collider zoneCollider) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView, zoneCollider) { }
        
        protected override void OnEnterStateRender()
            => StairsZoneView.ToggleVisibility(false);
    }
}
using App.Entities.Player;
using Fusion;
using UnityEngine;

namespace App.StairsZone.FSM.SpecificStates
{
    public class Idle : StairsZoneState
    {
        public Idle(NetStairsZone netStairsZone, StairsZoneConfig config, 
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView, Collider zoneCollider) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView, zoneCollider) { }
        
        protected override void OnEnterState() 
            => NetStairsZone.ExtractionTimer = TickTimer.None;

        protected override void OnEnterStateRender()
        {
            StairsZoneView.ToggleVisibility(true);
            StairsZoneView.ToggleCountdownVisibility(false);
        }

        protected override void OnFixedUpdate()
        {
            if (!HasKnockoutPlayer() && AllAlivePlayersInZone())
            {
                TryActivateState<Countdown>();
                return;
            }
        }
    }
}
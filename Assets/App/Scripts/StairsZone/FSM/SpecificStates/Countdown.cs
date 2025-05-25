using App.Entities.Player;
using Fusion;
using UnityEngine;

namespace App.StairsZone.FSM.SpecificStates
{
    public class Countdown : StairsZoneState
    {
        public Countdown(NetStairsZone netStairsZone, StairsZoneConfig config, 
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView, Collider zoneCollider) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView, zoneCollider) { }

        protected override void OnEnterState() 
            => NetStairsZone.ExtractionTimer = TickTimer.CreateFromSeconds(Runner, ExtractionTime);

        protected override void OnEnterStateRender()
        {
            StairsZoneView.ToggleVisibility(true);
            StairsZoneView.ToggleCountdownVisibility(true);
        }

        protected override void OnExitState()
        {
            NetStairsZone.ExtractionTimer = TickTimer.None;
        }

        protected override void OnFixedUpdate()
        {
            if (HasKnockoutPlayer() || !AllAlivePlayersInZone())
            {
                TryActivateState<Idle>();
                return;
            }

            if (NetStairsZone.ExtractionTimer.Expired(Runner))
            {
                TryActivateState<MovePlayers>();
                return;
            }
        }
    }
}
using App.Entities.Player;
using App.Missions.MissionState;
using Fusion;
using UnityEngine;

namespace App.ExtractionZone.FSM.SpecificStates
{
    public class Countdown : ExtractionZoneState
    {
        public Countdown(NetExtractionZone netExtractionZone, ExtractionZoneConfig config, NetMissionState netMissionState,
            PlayersEntitiesRepository playersEntitiesRepository, ExtractionZoneView extractionZoneView, Collider zoneCollider) 
            : base(netExtractionZone, config, netMissionState, playersEntitiesRepository, extractionZoneView, zoneCollider) { }

        protected override void OnEnterState() 
            => NetExtractionZone.ExtractionTimer = TickTimer.CreateFromSeconds(Runner, ExtractionTime);

        protected override void OnEnterStateRender()
            => ExtractionZoneView.ToggleCountdownVisibility(true);

        protected override void OnExitState()
        {
            NetExtractionZone.ExtractionTimer = TickTimer.None;
        }

        protected override void OnFixedUpdate()
        {
            if (!NetMissionState.IsRunning)
                return;
            
            if (!AllPlayersInZone())
            {
                TryActivateState<Idle>();
                return;
            }

            if (NetExtractionZone.ExtractionTimer.Expired(Runner))
            {
                TryActivateState<Extract>();
                return;
            }
        }
    }
}
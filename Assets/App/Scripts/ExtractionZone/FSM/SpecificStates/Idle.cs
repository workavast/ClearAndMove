using App.Entities.Player;
using App.Missions.MissionState;
using Fusion;
using UnityEngine;

namespace App.ExtractionZone.FSM.SpecificStates
{
    public class Idle : ExtractionZoneState
    {
        public Idle(NetExtractionZone netExtractionZone, ExtractionZoneConfig config, NetMissionState netMissionState,
            PlayersEntitiesRepository playersEntitiesRepository, ExtractionZoneView extractionZoneView, Collider zoneCollider) 
            : base(netExtractionZone, config, netMissionState, playersEntitiesRepository, extractionZoneView, zoneCollider) { }
        
        protected override void OnEnterState() 
            => NetExtractionZone.ExtractionTimer = TickTimer.None;

        protected override void OnEnterStateRender() 
            => ExtractionZoneView.ToggleCountdownVisibility(false);

        protected override void OnFixedUpdate()
        {
            if (!NetMissionState.IsRunning)
                return;

            if (AllPlayersInZone())
            {
                TryActivateState<Countdown>();
                return;
            }
        }
    }
}
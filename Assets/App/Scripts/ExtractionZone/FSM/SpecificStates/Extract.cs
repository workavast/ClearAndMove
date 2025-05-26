using App.Entities.Player;
using App.Missions.MissionState;
using UnityEngine;

namespace App.ExtractionZone.FSM.SpecificStates
{
    public class Extract : ExtractionZoneState
    {
        public Extract(NetExtractionZone netExtractionZone, ExtractionZoneConfig config, NetMissionState netMissionState,
            PlayersEntitiesRepository playersEntitiesRepository, ExtractionZoneView extractionZoneView, Collider zoneCollider) 
            : base(netExtractionZone, config, netMissionState, playersEntitiesRepository, extractionZoneView, zoneCollider) { }

        protected override void OnEnterState()
        {
            NetMissionState.FinishGame();
            TryActivateState<IsOver>();
        }
    }
}
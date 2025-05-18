using App.Entities.Player;
using App.Missions.MissionState;

namespace App.ExtractionZone.FSM.SpecificStates
{
    public class IsOver : ExtractionZoneState
    {
        public IsOver(NetExtractionZone netExtractionZone, ExtractionZoneConfig config, NetMissionState netMissionState,
            PlayersEntitiesRepository playersEntitiesRepository, ExtractionZoneView extractionZoneView) 
            : base(netExtractionZone, config, netMissionState, playersEntitiesRepository, extractionZoneView) { }

        protected override void OnEnterStateRender()
            => ExtractionZoneView.ToggleCountdownVisibility(true);
    }
}
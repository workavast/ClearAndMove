using App.Entities.Player;

namespace App.StairsZone.FSM.SpecificStates
{
    public class UnActive : StairsZoneState
    {
        public UnActive(NetStairsZone netStairsZone, StairsZoneConfig config, 
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView) { }

        protected override void OnEnterStateRender()
            => StairsZoneView.ToggleVisibility(false);
    }
}
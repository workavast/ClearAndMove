using App.Entities.Player;
using Fusion;

namespace App.StairsZone.FSM.SpecificStates
{
    public class Idle : StairsZoneState
    {
        public Idle(NetStairsZone netStairsZone, StairsZoneConfig config,
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView) { }
        
        protected override void OnEnterState() 
            => NetStairsZone.ExtractionTimer = TickTimer.None;

        protected override void OnEnterStateRender()
        {
            StairsZoneView.ToggleVisibility(true);
            StairsZoneView.ToggleCountdownVisibility(false);
        }

        protected override void OnFixedUpdate()
        {
            if (AllPlayersInZone())
            {
                TryActivateState<Countdown>();
                return;
            }
        }
    }
}
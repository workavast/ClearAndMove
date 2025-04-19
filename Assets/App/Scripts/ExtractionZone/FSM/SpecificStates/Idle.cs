using App.Entities.Player;
using App.GameState;
using Fusion;

namespace App.ExtractionZone.FSM.SpecificStates
{
    public class Idle : ExtractionZoneState
    {
        public Idle(NetExtractionZone netExtractionZone, ExtractionZoneConfig config, NetGameState netGameState,
            PlayersEntitiesRepository playersEntitiesRepository, ExtractionZoneView extractionZoneView) 
            : base(netExtractionZone, config, netGameState, playersEntitiesRepository, extractionZoneView) { }
        
        protected override void OnEnterState() 
            => NetExtractionZone.ExtractionTimer = TickTimer.None;

        protected override void OnEnterStateRender() 
            => ExtractionZoneView.ToggleCountdownVisibility(false);

        protected override void OnFixedUpdate()
        {
            if (!NetGameState.IsRunning)
                return;

            if (AllPlayersInZone())
            {
                TryActivateState<Countdown>();
                return;
            }
        }
    }
}
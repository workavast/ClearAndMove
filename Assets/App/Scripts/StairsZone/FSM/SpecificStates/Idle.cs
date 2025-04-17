using App.Entities.Player;
using App.NewDirectory1;
using Fusion;

namespace App.StairsZone.FSM.SpecificStates
{
    public class Idle : StairsZoneState
    {
        public Idle(NetStairsZone netStairsZone, StairsZoneConfig config, NetGameState netGameState,
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView) 
            : base(netStairsZone, config, netGameState, playersEntitiesRepository, stairsZoneView) { }
        
        protected override void OnEnterState() 
            => NetStairsZone.ExtractionTimer = TickTimer.None;

        protected override void OnEnterStateRender() 
            => StairsZoneView.ToggleCountdownVisibility(false);

        protected override void OnFixedUpdate()
        {
            if (!NetGameState.GameIsRunning)
                return;

            if (AllPlayersInZone())
            {
                TryActivateState<Countdown>();
                return;
            }
        }
    }
}
using App.Entities.Player;
using App.NewDirectory1;
using Fusion;

namespace App.StairsZone.FSM.SpecificStates
{
    public class Countdown : StairsZoneState
    {
        public Countdown(NetStairsZone netStairsZone, StairsZoneConfig config,
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView) { }

        protected override void OnEnterState() 
            => NetStairsZone.ExtractionTimer = TickTimer.CreateFromSeconds(Runner, ExtractionTime);

        protected override void OnEnterStateRender()
            => StairsZoneView.ToggleCountdownVisibility(true);

        protected override void OnExitState()
        {
            NetStairsZone.ExtractionTimer = TickTimer.None;
        }

        protected override void OnFixedUpdate()
        {
            if (!AllPlayersInZone())
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
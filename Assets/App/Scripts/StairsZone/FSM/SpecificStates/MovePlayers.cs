using App.Entities.Player;

namespace App.StairsZone.FSM.SpecificStates
{
    public class MovePlayers : StairsZoneState
    {
        public MovePlayers(NetStairsZone netStairsZone, StairsZoneConfig config,
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView) 
            : base(netStairsZone, config, playersEntitiesRepository, stairsZoneView) { }

        protected override void OnEnterState()
        {
            NetStairsZone.Mission.MoveToTheNextLevel();
        }

        protected override void OnFixedUpdate()
        {
            TryActivateState<Idle>();
        }
    }
}
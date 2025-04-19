using App.DeathChecking;
using App.Missions;
using Avastrad.EventBusFramework;
using Fusion;

namespace App.GameState.FSM.MissionStates
{
    public class MissionIsRunning : IsRunning
    {
        private readonly DeathChecker _deathChecker;
        private readonly Mission _mission;
        
        public MissionIsRunning(NetworkBehaviour networkBehaviour, IEventBus eventBus, DeathChecker deathChecker, Mission mission)
            : base(networkBehaviour, eventBus)
        {
            _deathChecker = deathChecker;
            _mission = mission;
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (_deathChecker.AllPlayersUnAlive)
            {
                TryActivateState<IsOver>();
                return;
            }

            if (_mission.AllEnemiesIsDead)
            {
                TryActivateState<IsOver>();
                return;
            }
        }
    }
}
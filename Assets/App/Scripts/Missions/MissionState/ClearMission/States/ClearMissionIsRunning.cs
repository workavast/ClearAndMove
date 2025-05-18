using App.DeathChecking;
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.ClearMission.States
{
    public class ClearMissionIsRunning : IsRunning
    {
        private readonly DeathChecker _deathChecker;
        private readonly Mission _mission;
        
        public ClearMissionIsRunning(NetworkBehaviour networkBehaviour, IEventBus eventBus, DeathChecker deathChecker, Mission mission)
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
                TryActivateState<IsFail>();
                return;
            }

            if (_mission.AllEnemiesIsDead)
            {
                TryActivateState<IsCompleted>();
                return;
            }
        }
    }
}
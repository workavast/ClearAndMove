using App.DeathChecking;
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.EvacuationMission.States
{
    public class EvacuationMissionIsRunning : IsRunning
    {
        private readonly PlayerEntitiesDeathChecker _deathChecker;

        public EvacuationMissionIsRunning(NetworkBehaviour networkBehaviour, IEventBus eventBus, PlayerEntitiesDeathChecker deathChecker) 
            : base(networkBehaviour, eventBus)
        {
            _deathChecker = deathChecker;
        }
        
        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (_deathChecker.AllPlayersUnAlive)
                TryActivateState<IsFail>();
        }
    }
}
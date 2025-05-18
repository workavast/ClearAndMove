using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.EvacuationMission.States
{
    public class EvacuationMissionIsPreparing : IsPreparing
    {
        public EvacuationMissionIsPreparing(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }

        protected override void OnFixedUpdate()
        {
            TryActivateState<IsRunning>();
        }
    }
}
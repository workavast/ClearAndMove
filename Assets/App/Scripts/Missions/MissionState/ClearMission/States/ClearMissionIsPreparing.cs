using App.Missions.MissionGeneration;
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.ClearMission.States
{
    public class ClearMissionIsPreparing : IsPreparing
    {
        private readonly NetMissionGenerator _netMissionGenerator;

        public ClearMissionIsPreparing(NetworkBehaviour networkBehaviour, IEventBus eventBus, NetMissionGenerator netMissionGenerator) 
            : base(networkBehaviour, eventBus)
        {
            _netMissionGenerator = netMissionGenerator;
        }
        
        protected override void OnFixedUpdate()
        {
            if (_netMissionGenerator.MissionGenerated) 
                TryActivateState<ClearMissionIsRunning>();
        }
    }
}
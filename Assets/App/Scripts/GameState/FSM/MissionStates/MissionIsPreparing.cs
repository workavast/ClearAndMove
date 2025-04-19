using App.Missions.MissionGeneration;
using Avastrad.EventBusFramework;
using Fusion;

namespace App.GameState.FSM.MissionStates
{
    public class MissionIsPreparing : IsPreparing
    {
        private readonly NetMissionGenerator _netMissionGenerator;

        public MissionIsPreparing(NetworkBehaviour networkBehaviour, IEventBus eventBus, NetMissionGenerator netMissionGenerator) 
            : base(networkBehaviour, eventBus)
        {
            _netMissionGenerator = netMissionGenerator;
        }
        
        protected override void OnFixedUpdate()
        {
            if (_netMissionGenerator.MissionGenerated) 
                TryActivateState<MissionIsRunning>();
        }
    }
}
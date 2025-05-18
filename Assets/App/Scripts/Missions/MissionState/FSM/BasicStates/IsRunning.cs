using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.FSM
{
    public abstract class IsRunning : MissionStateBase
    {
        protected IsRunning(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }
    }
}
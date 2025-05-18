using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.FSM
{
    public abstract class IsCompleted : MissionStateBase
    {
        protected IsCompleted(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }
    }
}
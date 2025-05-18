using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.FSM
{
    public abstract class IsFail : MissionStateBase
    {
        protected IsFail(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }
    }
}
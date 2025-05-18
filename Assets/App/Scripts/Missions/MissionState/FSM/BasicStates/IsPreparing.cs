using Avastrad.EventBusFramework;
using Fusion;

namespace App.Missions.MissionState.FSM
{
    public abstract class IsPreparing : MissionStateBase
    {
        protected IsPreparing(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }
    }
}
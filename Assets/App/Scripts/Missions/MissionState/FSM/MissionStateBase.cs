using App.EventBus;
using Avastrad.EventBusFramework;
using Fusion;
using Fusion.Addons.FSM;

namespace App.Missions.MissionState.FSM
{
    public abstract class MissionStateBase : State<MissionStateBase>
    {
        private readonly NetworkBehaviour _owner;
        private readonly IEventBus _eventBus;
        
        protected NetworkRunner Runner => _owner.Runner;
        
        protected MissionStateBase(NetworkBehaviour networkBehaviour, IEventBus eventBus)
        {
            _owner = networkBehaviour;
            _eventBus = eventBus;
        }
        
        protected override void OnEnterState()
        {
            _eventBus.Invoke(new OnGameStateChanged(GetType()));
        }
    }
}
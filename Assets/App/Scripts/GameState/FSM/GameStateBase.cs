using App.EventBus;
using Avastrad.EventBusFramework;
using Fusion;
using Fusion.Addons.FSM;

namespace App.GameState.FSM
{
    public abstract class GameStateBase : State<GameStateBase>
    {
        protected abstract bool GameIsRunning { get; }
        
        private readonly NetworkBehaviour _owner;
        private readonly IEventBus _eventBus;
        
        protected NetworkRunner Runner => _owner.Runner;
        
        protected GameStateBase(NetworkBehaviour networkBehaviour, IEventBus eventBus)
        {
            _owner = networkBehaviour;
            _eventBus = eventBus;
        }
        
        protected override void OnEnterState()
        {
            _eventBus.Invoke(new OnGameStateChanged(GameIsRunning));
        }
    }
}
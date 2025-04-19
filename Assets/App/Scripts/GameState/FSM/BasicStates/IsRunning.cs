using Avastrad.EventBusFramework;
using Fusion;

namespace App.GameState.FSM
{
    public abstract class IsRunning : GameStateBase
    {
        protected override bool GameIsRunning => true;

        protected IsRunning(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }
    }
}
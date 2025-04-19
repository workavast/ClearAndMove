using Avastrad.EventBusFramework;
using Fusion;

namespace App.GameState.FSM
{
    public abstract class IsPreparing : GameStateBase
    {
        protected override bool GameIsRunning => false;

        protected IsPreparing(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }
    }
}
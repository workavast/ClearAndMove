using Avastrad.EventBusFramework;
using Fusion;

namespace App.GameState.FSM
{
    public abstract class IsOver : GameStateBase
    {
        protected override bool GameIsRunning => false;

        protected IsOver(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }
    }
}
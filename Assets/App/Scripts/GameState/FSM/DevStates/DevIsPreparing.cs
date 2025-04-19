using Avastrad.EventBusFramework;
using Fusion;

namespace App.GameState.FSM.DevStates
{
    public class DevIsPreparing : IsPreparing
    {
        public DevIsPreparing(NetworkBehaviour networkBehaviour, IEventBus eventBus) 
            : base(networkBehaviour, eventBus) { }

        protected override void OnFixedUpdate()
        {
            TryActivateState<IsRunning>();
        }
    }
}
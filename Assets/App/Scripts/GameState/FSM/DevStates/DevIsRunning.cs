using App.DeathChecking;
using Avastrad.EventBusFramework;
using Fusion;

namespace App.GameState.FSM.DevStates
{
    public class DevIsRunning : IsRunning
    {
        private readonly DeathChecker _deathChecker;

        public DevIsRunning(NetworkBehaviour networkBehaviour, IEventBus eventBus, DeathChecker deathChecker) 
            : base(networkBehaviour, eventBus)
        {
            _deathChecker = deathChecker;
        }
        
        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (_deathChecker.AllPlayersUnAlive)
                TryActivateState<IsOver>();
        }
    }
}
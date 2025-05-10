using Fusion;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class Idle : MissionGenerationState
    {
        public Idle(NetworkBehaviour netOwner) 
            : base(netOwner) { }

        protected override void OnFixedUpdate()
        {
            TryActivateState<PrepareGeneration>();
        }
    }
}
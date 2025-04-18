using Fusion;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class Idle : MissionGenerationState
    {
        public Idle(NetworkBehaviour netHealth) 
            : base(netHealth) { }

        protected override void OnFixedUpdate()
        {
            TryActivateState<Generation>();
        }
    }
}
using Fusion;

namespace App.Missions.MissionGeneration.FSM.SpecificStates
{
    public class GenerationIsOver : MissionGenerationState
    {
        public GenerationIsOver(NetworkBehaviour netOwner) 
            : base(netOwner) { }
    }
}
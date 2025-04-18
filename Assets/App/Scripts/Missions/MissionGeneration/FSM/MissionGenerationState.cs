using Fusion;
using Fusion.Addons.FSM;

namespace App.Missions.MissionGeneration.FSM
{
    public abstract class MissionGenerationState : State<MissionGenerationState>
    {
        private readonly NetworkBehaviour _owner;
        
        protected NetworkRunner Runner => _owner.Runner;
        
        protected MissionGenerationState(NetworkBehaviour netHealth)
        {
            _owner = netHealth;
        }
    }
}
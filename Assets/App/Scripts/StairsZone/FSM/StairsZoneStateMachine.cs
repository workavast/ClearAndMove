using Fusion.Addons.FSM;

namespace App.StairsZone.FSM
{
    public class StairsZoneStateMachine : StateMachine<StairsZoneState>
    {
        public StairsZoneStateMachine(string name, params StairsZoneState[] states)
            : base(name, states) { }
    }
}
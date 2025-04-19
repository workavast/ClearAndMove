using App.GameState.FSM;
using App.GameState.FSM.DevStates;

namespace App.GameState
{
    public class NetDevState : NetGameState
    {
        protected override IsPreparing _isPreparing { get; set; }
        protected override IsRunning _isRunning { get; set; }
        protected override IsOver _isOver { get; set; }

        public override void CreateStates()
        {
            _isPreparing = new DevIsPreparing(this, _eventBus);
            _isRunning = new DevIsRunning(this, _eventBus, deathChecker);
            _isOver = new DevIsOver(this, _eventBus, _screensController);
        }
    }
}
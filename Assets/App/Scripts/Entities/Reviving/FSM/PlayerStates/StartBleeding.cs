using App.Health;

namespace App.Entities.Reviving.FSM.PlayerStates
{
    public class StartBleeding : PlayerReviveState
    {
        private readonly ReviveConfig _config;

        public StartBleeding(NetReviver netReviver, NetHealth netHealth, ReviveView reviveView, ReviveConfig config) 
            : base(netReviver, netHealth, reviveView)
        {
            _config = config;
        }

        protected override void OnEnterState() 
            => NetReviver.BleedTimer = _config.BleedingTime;

        protected override void OnEnterStateRender()
        {
            if (ReviveView != null)
                ReviveView.ToggleVisibility(false);
        }

        protected override void OnFixedUpdate() 
            => TryActivateState<Bleeding>();
    }
}
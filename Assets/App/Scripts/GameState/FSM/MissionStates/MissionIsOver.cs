using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;
using Fusion;

namespace App.GameState.FSM.MissionStates
{
    public class MissionIsOver : IsOver
    {
        private readonly ScreensController _screensController;

        public MissionIsOver(NetworkBehaviour networkBehaviour, IEventBus eventBus, ScreensController screensController) 
            : base(networkBehaviour, eventBus)
        {
            _screensController = screensController;
        }

        protected override void OnEnterStateRender()
        {
            _screensController.SetScreen(ScreenType.EndGame);
        }
    }
}
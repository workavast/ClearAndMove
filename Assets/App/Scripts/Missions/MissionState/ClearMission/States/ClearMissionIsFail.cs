using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;
using Fusion;

namespace App.Missions.MissionState.ClearMission.States
{
    public class ClearMissionIsFail : IsFail
    {
        private readonly ScreensController _screensController;

        public ClearMissionIsFail(NetworkBehaviour networkBehaviour, IEventBus eventBus, ScreensController screensController) 
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
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;
using Fusion;

namespace App.Missions.MissionState.EvacuationMission.States
{
    public class EvacuationMissionIsFail : IsFail
    {
        private readonly ScreensController _screensController;

        public EvacuationMissionIsFail(NetworkBehaviour networkBehaviour, IEventBus eventBus, ScreensController screensController) 
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
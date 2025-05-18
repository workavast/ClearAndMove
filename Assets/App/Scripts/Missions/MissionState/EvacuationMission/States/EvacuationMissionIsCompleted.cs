using App.Missions.MissionsProgress;
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;
using Fusion;

namespace App.Missions.MissionState.EvacuationMission.States
{
    public class EvacuationMissionIsCompleted : IsCompleted
    {
        private readonly ScreensController _screensController;
        private readonly CompletedMissionsModel _completedMissionsModel;
        private readonly Mission _mission;

        public EvacuationMissionIsCompleted(NetworkBehaviour networkBehaviour, IEventBus eventBus, ScreensController screensController, CompletedMissionsModel completedMissionsModel, Mission mission) 
            : base(networkBehaviour, eventBus)
        {
            _screensController = screensController;
            _completedMissionsModel = completedMissionsModel;
            _mission = mission;
        }
        
        protected override void OnEnterStateRender()
        {
            _completedMissionsModel.SetMissionState(_mission.GetMissionIndex(), true);
            _screensController.SetScreen(ScreenType.EndGame);
        }
    }
}
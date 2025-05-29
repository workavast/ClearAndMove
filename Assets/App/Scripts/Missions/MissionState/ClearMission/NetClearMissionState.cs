using App.DeathChecking;
using App.Missions.MissionGeneration;
using App.Missions.MissionsProgress;
using App.Missions.MissionState.ClearMission.States;
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;
using UnityEngine;
using Zenject;

namespace App.Missions.MissionState.ClearMission
{
    public class NetClearMissionState : NetMissionState
    {
        [SerializeField] private Mission mission;
        [SerializeField] private NetMissionGenerator netMissionGenerator;
        [SerializeField] private PlayerEntitiesDeathChecker deathChecker;

        [Inject] private readonly CompletedMissionsModel _completedMissionsModel;
        [Inject] private readonly ScreensController _screensController;
        [Inject] private readonly IEventBus _eventBus;
        
        protected override IsPreparing _isPreparing { get; set; }
        protected override IsRunning _isRunning { get; set; }
        protected override IsCompleted _isCompleted { get; set; }
        protected override IsFail _isFail { get; set; }

        public override void CreateStates()
        {
            _isPreparing = new ClearMissionIsPreparing(this, _eventBus, netMissionGenerator);
            _isRunning = new ClearMissionIsRunning(this, _eventBus, deathChecker, mission);
            _isCompleted = new ClearMissionIsCompleted(this, _eventBus, _screensController, _completedMissionsModel, mission);
            _isFail = new ClearMissionIsFail(this, _eventBus, _screensController);
        }
    }
}
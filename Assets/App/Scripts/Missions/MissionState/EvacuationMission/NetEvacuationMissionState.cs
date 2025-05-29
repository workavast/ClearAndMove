using App.DeathChecking;
using App.Missions.MissionsProgress;
using App.Missions.MissionState.EvacuationMission.States;
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;
using UnityEngine;
using Zenject;

namespace App.Missions.MissionState.EvacuationMission
{
    public class NetEvacuationMissionState : NetMissionState
    {
        [SerializeField] private PlayerEntitiesDeathChecker deathChecker;
        [SerializeField] private Mission mission;
        
        [Inject] private readonly CompletedMissionsModel _completedMissionsModel;
        [Inject] private readonly ScreensController _screensController;
        [Inject] private readonly IEventBus _eventBus;
        
        protected override IsPreparing _isPreparing { get; set; }
        protected override IsRunning _isRunning { get; set; }
        protected override IsCompleted _isCompleted { get; set; }
        protected override IsFail _isFail { get; set; }

        public override void CreateStates()
        {
            _isPreparing = new EvacuationMissionIsPreparing(this, _eventBus);
            _isRunning = new EvacuationMissionIsRunning(this, _eventBus, deathChecker);
            _isCompleted = new EvacuationMissionIsCompleted(this, _eventBus, _screensController, _completedMissionsModel, mission);
            _isFail = new EvacuationMissionIsFail(this, _eventBus, _screensController);
        }
    }
}
using App.GameState.FSM;
using App.GameState.FSM.MissionStates;
using App.Missions;
using App.Missions.MissionGeneration;
using UnityEngine;

namespace App.GameState
{
    public class NetMissionState : NetGameState
    {
        [SerializeField] private Mission mission;
        [SerializeField] private NetMissionGenerator netMissionGenerator;
        
        protected override IsPreparing _isPreparing { get; set; }
        protected override IsRunning _isRunning { get; set; }
        protected override IsOver _isOver { get; set; }

        public override void CreateStates()
        {
            _isPreparing = new MissionIsPreparing(this, _eventBus, netMissionGenerator);
            _isRunning = new MissionIsRunning(this, _eventBus, deathChecker, mission);
            _isOver = new MissionIsOver(this, _eventBus, _screensController);
        }
    }
}
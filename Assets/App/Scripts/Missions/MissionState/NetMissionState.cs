using System.Collections.Generic;
using App.Missions.MissionState.FSM;
using Fusion;
using Fusion.Addons.FSM;

namespace App.Missions.MissionState
{
    public abstract class NetMissionState : NetworkBehaviour, IStateMachineOwner
    {
        public bool IsPreparing => !IsSpawned || _fsm.ActiveState == _isPreparing;
        public bool IsRunning => IsSpawned && _fsm.ActiveState == _isRunning;
        public bool IsCompleted => IsSpawned && _fsm.ActiveState == _isCompleted;
        public bool IsFail => IsSpawned && _fsm.ActiveState == _isFail;
        public bool IsOver => IsCompleted || IsFail;

        public bool IsSpawned { get; private set; }

        private StateMachine<MissionStateBase> _fsm;
        protected abstract IsPreparing _isPreparing { get; set; }
        protected abstract IsRunning _isRunning { get; set; }
        protected abstract IsCompleted _isCompleted { get; set; }
        protected abstract IsFail _isFail { get; set; }

        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            CreateStates();
            _fsm = new StateMachine<MissionStateBase>("Game", _isPreparing, _isRunning, _isCompleted, _isFail);
            stateMachines.Add(_fsm);
        }

        public override void Spawned() 
            => IsSpawned = true;

        public override void Despawned(NetworkRunner runner, bool hasState) 
            => IsSpawned = false;

        public abstract void CreateStates();
        
        public void FinishGame()
        {
            if (HasStateAuthority)
                _fsm.TryActivateState(_isCompleted);
        }
    }
}
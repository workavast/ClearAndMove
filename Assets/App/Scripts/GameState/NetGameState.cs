using System.Collections.Generic;
using App.DeathChecking;
using App.GameState.FSM;
using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;
using Zenject;

namespace App.GameState
{
    public abstract class NetGameState : NetworkBehaviour, IStateMachineOwner
    {
        [SerializeField] protected DeathChecker deathChecker;

        [Inject] protected readonly ScreensController _screensController;
        [Inject] protected readonly IEventBus _eventBus;

        public bool IsPreparing => !IsSpawned || _fsm.ActiveState == _isPreparing;
        public bool IsRunning => IsSpawned && _fsm.ActiveState == _isRunning;
        public bool IsOver => IsSpawned && _fsm.ActiveState == _isOver;

        public bool IsSpawned { get; private set; }

        private StateMachine<GameStateBase> _fsm;
        protected abstract IsPreparing _isPreparing { get; set; }
        protected abstract IsRunning _isRunning { get; set; }
        protected abstract IsOver _isOver { get; set; }

        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            CreateStates();
            _fsm = new StateMachine<GameStateBase>("Game", _isPreparing, _isRunning, _isOver);
            stateMachines.Add(_fsm);
        }

        public override void Spawned()
        {
            IsSpawned = true;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            IsSpawned = false;
        }

        public abstract void CreateStates();
        
        public void FinishGame()
        {
            if (HasStateAuthority)
                _fsm.TryActivateState(_isOver);
        }
    }
}
using System.Collections.Generic;
using App.Ai.Config;
using App.Ai.FSMs.Ai;
using App.Entities;
using App.EventBus;
using Avastrad.EventBusFramework;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;

namespace App.Ai
{
    [RequireComponent(typeof(StateMachineController))]
    public class NetAi : NetworkBehaviour, IStateMachineOwner, IEventReceiver<OnGameStateChanged>
    {
        [SerializeField] private AiConfig aiConfig;
        [SerializeField] private NetEntity netEntity;
        [SerializeField] private AiViewZone aiViewZone;
     
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        
        private AiModel _aiModel;
        
        private StateMachine<AiState> _fsm;
        private Idle _idle;
        private ChaseState _chase;
        private HoldPositionState _holdPosition;
        private CombatState _combat;
        private Stop _stop;

        private void Awake()
        {
            _aiModel = new AiModel(aiConfig);
        }

        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            _idle = new Idle(this, netEntity, _aiModel, aiViewZone);
            _chase = new ChaseState(this, netEntity, _aiModel, aiViewZone);
            _holdPosition = new HoldPositionState(this, netEntity, _aiModel, aiViewZone);
            _combat = new CombatState(this, netEntity, _aiModel, aiViewZone);
            _stop = new Stop(this, netEntity, _aiModel, aiViewZone);
            
            _fsm = new StateMachine<AiState>("Ai", _idle, _chase, _holdPosition, _combat, _stop);
            stateMachines.Add(_fsm);
        }

         public override void FixedUpdateNetwork()
         {
             if (!netEntity.IsAlive && _fsm.ActiveState != null && _fsm.ActiveState != _stop)
             {
                 _fsm.TryActivateState<Stop>();
             }
         }

         public void OnEvent(OnGameStateChanged e)
         {
             if (!e.IsRunning && _fsm.ActiveState != null && _fsm.ActiveState != _stop) 
                 _fsm.TryActivateState<Stop>();
         }
    }
}
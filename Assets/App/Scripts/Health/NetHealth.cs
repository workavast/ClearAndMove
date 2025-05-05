using System;
using System.Collections.Generic;
using App.Entities;
using App.EventBus;
using App.Health.FSM;
using App.Health.FSM.SpecificStates;
using Avastrad.EventBusFramework;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;
using Zenject;

namespace App.Health
{
    public class NetHealth : NetworkBehaviour, IStateMachineOwner
    {
        [SerializeField] private EntityConfig config;
        [SerializeField] private NetEntity entity;
        [SerializeField] private SolderView solderView;
     
        [Networked] [field: ReadOnly] public float NetHealthPoints { get; private set; }

        [Inject] private readonly IEventBus _eventBus;

        public bool IsAlive => _fsm.ActiveState == _alive;
        public bool IsKnockout => _fsm.ActiveState == _knockout;
        public bool IsDead => _fsm.ActiveState == _dead;

        public float MaxHealthPoints => config.MaxHealthPoints;
        
        private HealthStateMachine _fsm;
        private Alive _alive;
        private Knockout _knockout;
        private Dead _dead;

        private IEntity _lastDamager;
        
        public event Action OnKnockout;
        public event Action OnDeath;
        public event Action<IEntity> OnDeathEntity;
        
        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            _alive = new Alive(this);
            _knockout = new Knockout(this, config);
            _dead = new Dead(this);

            _knockout.OnActivate += () => OnKnockout?.Invoke();
            
            _fsm = new HealthStateMachine("Health", _alive, _knockout, _dead);
            
            stateMachines.Add(_fsm);
        }

        public override void Spawned()
        {
            NetHealthPoints = config.MaxHealthPoints;
            solderView.SetAliveState(IsAlive);
        }

        public override void Render()
        {
            solderView.SetAliveState(IsAlive);
        }

        public void SetHealth(int value)
        {
            NetHealthPoints = value;
        }
        
        public void TakeDamage(float damage, IEntity killer)
        {
            if (_fsm.ActiveState != _alive)
            {
                Debug.LogError($"You try damage entity that un alive: [{gameObject.name}] [{_fsm.ActiveState.GetType()}]");
                return;
            }
            
            NetHealthPoints -= damage;
            
            if (HasStateAuthority && NetHealthPoints <= 0)
            {
                _lastDamager = killer;
            }
        }

        public void PermanentDeath()
        {
            NetHealthPoints = 0;
            if (HasStateAuthority)
            {
                _fsm.TryActivateState<Dead>();
                _eventBus.Invoke(new OnKill(entity.Identifier.Id, _lastDamager.Identifier.Id));
            }
        }

        public void PermanentDeathRenderer()
        {
            Debug.Log($"{entity.GetName()} is dead");
            OnDeath?.Invoke();
            OnDeathEntity?.Invoke(entity);
        }

        public void Revive()
        {
            SetHealth(config.ReviveHealthPoints);
            _fsm.TryActivateState<Alive>();
        }
    }
}
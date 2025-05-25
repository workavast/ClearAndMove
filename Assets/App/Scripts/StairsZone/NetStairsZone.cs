using System;
using System.Collections.Generic;
using App.Core.Timer;
using App.Entities.Player;
using App.Missions;
using App.StairsZone.FSM;
using App.StairsZone.FSM.SpecificStates;
using DCFApixels;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;
using Zenject;

namespace App.StairsZone
{
    public class NetStairsZone : NetworkBehaviour, IStateMachineOwner, ITimer
    {
        [SerializeField] private StairsZoneConfig config;
        [SerializeField] private StairsZoneView stairsZoneView;
        [SerializeField] private Transform movePoint;
        [SerializeField] private Collider zoneCollider;

        [Networked] public TickTimer ExtractionTimer { get; set; }

        [Inject] private PlayersEntitiesRepository _playersEntitiesRepository;

        public Transform MovePoint => movePoint;
        public bool IsSpawned { get; private set; }
        public Mission Mission { get; private set; }

        private int _lastRemainingTime;
        private TimeSpan _lastTimeSPan;

        private UnActive _unActive;
        private Idle _idle;
        private Countdown _countdown;
        private MovePlayers _movePlayers;
        private StairsZoneStateMachine _fsm;

        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            _unActive = new UnActive(this, config, _playersEntitiesRepository, stairsZoneView, zoneCollider);
            _idle = new Idle(this, config, _playersEntitiesRepository, stairsZoneView, zoneCollider);
            _countdown = new Countdown(this, config, _playersEntitiesRepository, stairsZoneView, zoneCollider);
            _movePlayers = new MovePlayers(this, config, _playersEntitiesRepository, stairsZoneView, zoneCollider);

            _fsm = new StairsZoneStateMachine("Stairs Zone", _unActive, _idle, _countdown, _movePlayers);

            stateMachines.Add(_fsm);
        }

        public override void Spawned()
        {
            IsSpawned = true;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
            => IsSpawned = false;

        public void SetMission(Mission mission)
        {
            Mission = mission;
        }
        
        public void SetActivityState(bool isActive)
        {
            if (isActive) 
                _fsm.TryActivateState(_idle);
            else
                _fsm.TryActivateState(_unActive);
        }

        public void SetMovePoint(Transform newMovePoint)
        {
            movePoint = newMovePoint;
        }
        
        public TimeSpan GetTime()
        {
            if (!IsSpawned || ExtractionTimer.ExpiredOrNotRunning(Runner))
                return TimeSpan.Zero;

            var remainingTime = (int)Math.Floor(ExtractionTimer.RemainingTime(Runner).Value);
            if (_lastRemainingTime == remainingTime)
                return _lastTimeSPan;

            _lastRemainingTime = remainingTime;

            var minutes = (int)Math.Floor((float)_lastRemainingTime / 60);
            var seconds = _lastRemainingTime % 60;
            _lastTimeSPan = new TimeSpan(0, 0, minutes, seconds);

            return _lastTimeSPan;
        }

        private void OnDrawGizmos()
        {
            if (movePoint != null)
            {
                DebugX.Draw().Line(transform.position, movePoint.position);
                DebugX.Draw().Sphere(movePoint.position, 0.5f);
            }
        }
    }
}
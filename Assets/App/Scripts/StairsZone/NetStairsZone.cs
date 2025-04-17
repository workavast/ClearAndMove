using System;
using System.Collections.Generic;
using App.Core.Timer;
using App.Entities.Player;
using App.NewDirectory1;
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
        [SerializeField] private NetGameState netGameState;
        [SerializeField] private StairsZoneConfig config;
        [SerializeField] private StairsZoneView stairsZoneView;
        [SerializeField] private Transform movePoint;

        [Networked] public TickTimer ExtractionTimer { get; set; }

        [Inject] private PlayersEntitiesRepository _playersEntitiesRepository;

        public bool IsSpawned { get; private set; }

        private int _lastRemainingTime;
        private TimeSpan _lastTimeSPan;

        private Idle _idle;
        private Countdown _countdown;
        private MovePlayers _movePlayers;
        private StairsZoneStateMachine _fsm;

        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            _idle = new Idle(this, config, netGameState, _playersEntitiesRepository, stairsZoneView);
            _countdown = new Countdown(this, config, netGameState, _playersEntitiesRepository, stairsZoneView);
            _movePlayers = new MovePlayers(this, config, netGameState, _playersEntitiesRepository, stairsZoneView, movePoint);

            _fsm = new StairsZoneStateMachine("ExtractionZone", _idle, _countdown, _movePlayers);

            stateMachines.Add(_fsm);
        }

        public override void Spawned()
        {
            stairsZoneView.SetSize(config.ExtractionRadius);
            IsSpawned = true;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
            => IsSpawned = false;

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
            if (config != null)
                DebugX.Draw().WireSphere(transform.position, config.ExtractionRadius);

            if (movePoint != null)
            {
                DebugX.Draw().Line(transform.position, movePoint.position);
                DebugX.Draw().Sphere(movePoint.position, 0.5f);
            }
        }
    }
}
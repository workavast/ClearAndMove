using System;
using App.Missions.MissionState.FSM;
using Avastrad.EventBusFramework;

namespace App.EventBus
{
    public struct OnGameStateChanged : IEvent
    {
        public readonly bool IsPreparing;
        public readonly bool IsRunning;
        public readonly bool IsCompleted;
        public readonly bool IsFail;
        public readonly bool IsOver => IsCompleted || IsFail;
        
        public OnGameStateChanged(Type gameState)
        {
            IsPreparing = typeof(IsPreparing).IsAssignableFrom(gameState);
            IsRunning = typeof(IsRunning).IsAssignableFrom(gameState);
            IsCompleted = typeof(IsCompleted).IsAssignableFrom(gameState);
            IsFail = typeof(IsFail).IsAssignableFrom(gameState);
        }
    }
}
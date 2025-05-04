using System;
using App.GameState.FSM;
using Avastrad.EventBusFramework;

namespace App.EventBus
{
    public struct OnGameStateChanged : IEvent
    {
        public readonly bool IsPreparing;
        public readonly bool IsRunning;
        public readonly bool IsOver;

        public OnGameStateChanged(Type gameState)
        {
            IsPreparing = typeof(IsPreparing).IsAssignableFrom(gameState);
            IsRunning = typeof(IsRunning).IsAssignableFrom(gameState);
            IsOver = typeof(IsOver).IsAssignableFrom(gameState);
        }
    }
}
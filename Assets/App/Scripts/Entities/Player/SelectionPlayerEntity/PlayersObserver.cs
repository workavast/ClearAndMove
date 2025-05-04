using System;
using App.EventBus;
using App.GameState;
using App.NetworkRunning;
using Avastrad.EventBusFramework;
using Avastrad.UI.UiSystem;

namespace App.Entities.Player.SelectionPlayerEntity
{
    public class PlayersObserver : IEventReceiver<OnPlayerDeath>, IEventReceiver<OnGameStateChanged>, IDisposable
    {
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        
        private readonly IEventBus _eventBus;
        private readonly ScreensController _screensController;
        private readonly NetworkRunnerProvider _networkRunnerProvider;
        private readonly PlayersEntitiesRepository _playersEntitiesRepository;

        public PlayersObserver(IEventBus eventBus, ScreensController screensController, NetworkRunnerProvider networkRunnerProvider, PlayersEntitiesRepository playersEntitiesRepository, NetGameState netGameState)
        {
            _eventBus = eventBus;
            _screensController = screensController;
            _networkRunnerProvider = networkRunnerProvider;
            _playersEntitiesRepository = playersEntitiesRepository;

            _eventBus.Subscribe<OnPlayerDeath>(this);
            _eventBus.Subscribe<OnGameStateChanged>(this);
        }
        
        public void Dispose()
        {
            _eventBus.UnSubscribe<OnPlayerDeath>(this);
            _eventBus.UnSubscribe<OnGameStateChanged>(this);
        }

        public void OnEvent(OnGameStateChanged e)
        {
            if (e.IsOver) 
                Dispose();
        }

        
        public void OnEvent(OnPlayerDeath e)
        {
            if (_networkRunnerProvider.TryGetNetworkRunner(out var runner))
            {
                if (runner.LocalPlayer != e.PlayerRef)
                    return;
                
                _screensController.SetScreen(ScreenType.PlayersObserve);
            }
        }
    }
}
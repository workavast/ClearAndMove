using App.Lobby;
using App.Lobby.Missions.Map;
using App.Lobby.StartGameTimer;
using App.Localization;
using App.NetworkRunning;
using App.NetworkRunning.Shutdowners;
using App.NetworkRunning.Shutdowners.LocalShutdowners;
using App.Session.Creation;
using App.Session.Visibility;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace App.Bootstraps
{
    public class LobbyBootstrap : MonoBehaviour
    {
        [SerializeField] private StringTablesPreloader localizationPreloader;

        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly NetworkRunnerProvider _runnerProvider;
        [Inject] private readonly SessionCreator _sessionCreator;
        [Inject] private readonly IReadOnlyGameStartTimer _gameStartTimer;
        [Inject] private readonly IMissionsMapModel _missionsMapModel;
        [Inject] private readonly SessionVisibilityManager _sessionVisibilityManager;
        [Inject] private readonly ShutdownerProvider _shutdownerProvider;
  
        private GameStarter _gameStarter;
        
        private async void Start()
        {
            if (!_runnerProvider.TryGetNetworkRunner(out _))
                await _sessionCreator.CreateSinglePlayer(SceneManager.GetActiveScene().buildIndex, true);

            _shutdownerProvider.SetLocalShutdownProvider(new LobbyShutdowner(_sceneLoader));
            _sessionVisibilityManager.SetHardVisibility(true);
            
            if (_runnerProvider.TryGetNetworkRunner(out var runner) && runner.IsServer)
                _gameStarter = new GameStarter(_gameStartTimer, _sceneLoader, _missionsMapModel, _sessionVisibilityManager);

            await localizationPreloader.Preload();

            _sceneLoader.HideLoadScreen(true);
        }
        
        private void OnDestroy() 
            => localizationPreloader.Release();
    }
}
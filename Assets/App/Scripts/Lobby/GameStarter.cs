using System;
using App.Lobby.Missions.Map;
using App.Lobby.StartGameTimer;
using App.Session.Visibility;
using Avastrad.ScenesLoading;

namespace App.Lobby
{
    public class GameStarter : IDisposable
    {
        private readonly IReadOnlyGameStartTimer _startGameTimer;
        private readonly ISceneLoader _sceneLoader;
        private readonly IMissionsMapModel _missionsMapModel;
        private readonly SessionVisibilityManager _sessionVisibilityManager;

        public GameStarter(IReadOnlyGameStartTimer startGameTimer, ISceneLoader sceneLoader, 
            IMissionsMapModel missionsMapModel, SessionVisibilityManager sessionVisibilityManager)
        {
            _startGameTimer = startGameTimer;
            _sceneLoader = sceneLoader;
            _missionsMapModel = missionsMapModel;
            _sessionVisibilityManager = sessionVisibilityManager;

            _startGameTimer.OnTimerIsOver += StartGame;
        }

        private void StartGame()
        {
            _sessionVisibilityManager.SetHardVisibility(false);

            var missionConfig = _missionsMapModel.GetMission(_missionsMapModel.SelectedMissionIndex);
            _sceneLoader.LoadScene(missionConfig.SceneIndex);
        }

        public void Dispose()
        {
            _startGameTimer.OnTimerIsOver -= StartGame;
        }
    }
}
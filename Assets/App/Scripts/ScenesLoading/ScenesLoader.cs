using System;
using App.NetworkRunning;
using Avastrad.ScenesLoading;
using Fusion;
using UnityEngine.SceneManagement;

namespace App.ScenesLoading
{
    public class ScenesLoader : ISceneLoader
    {
        private readonly int _loadingSceneIndex;

        private readonly ILoadingScreen _loadingScreen;
        private readonly NetworkRunnerProvider _networkRunnerProvider;
        
        private int _targetSceneIndex = -1;
        
        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public ScenesLoader(ILoadingScreen loadingScreen, int loadingSceneIndex, 
            NetworkRunnerProvider networkRunnerProvider)
        {
            _loadingScreen = loadingScreen;
            _loadingSceneIndex = loadingSceneIndex;
            _networkRunnerProvider = networkRunnerProvider;
            
            _loadingScreen.OnHided += () => OnLoadingScreenHided?.Invoke();
        }

        public void ShowLoadScreen(bool showInstantly, Action onShowedCallback)
            => _loadingScreen.Show(showInstantly, onShowedCallback);

        public void HideLoadScreen(bool hideLoadScreenInstantly) 
            => _loadingScreen.Hide(hideLoadScreenInstantly);

        /// <param name="skipLoadingScene"> skip loading scene </param>
        public void LoadScene(int index, bool showLoadScreenInstantly = false, bool skipLoadingScene = false)
        {
            var hasNetRunner = false;
            if (_networkRunnerProvider.TryGetNetworkRunner(out var netRunner) &&
                netRunner.IsRunning && !netRunner.IsShutdown)
            {
                if (!netRunner.IsServer)
                    throw new Exception("You try load scene when you are not a server");
                hasNetRunner = true;
            }
            
            _targetSceneIndex = index;
            
            OnLoadingStarted?.Invoke();
            
            if (skipLoadingScene)
            {
                if (hasNetRunner)
                    netRunner.LoadScene(SceneRef.FromIndex(_targetSceneIndex));
                else
                    SceneManager.LoadScene(_targetSceneIndex);
            }
            else
            {
                _loadingScreen.Show(showLoadScreenInstantly, 
                    () =>
                    {
                        if (hasNetRunner)
                            netRunner.LoadScene(SceneRef.FromIndex(_loadingSceneIndex));
                        else
                            SceneManager.LoadSceneAsync(_loadingSceneIndex);
                    });  
            }
        }

        public void LoadTargetScene()
        {
            var netRunner = _networkRunnerProvider.GetNetworkRunner();

            if (netRunner.IsRunning && !netRunner.IsServer)
                return;

            if (netRunner.IsRunning) 
                netRunner.LoadScene(SceneRef.FromIndex(_targetSceneIndex));
            else
                SceneManager.LoadSceneAsync(_targetSceneIndex);
        }
    }
}
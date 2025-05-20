using App.NetworkRunning;
using App.NetworkRunning.Shutdowners;
using App.NetworkRunning.Shutdowners.LocalShutdowners;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class LoadingSceneBootstrap : MonoBehaviour
    {
        [Inject] private readonly NetworkRunnerProvider _runnerProvider;
        [Inject] private readonly ShutdownerProvider _shutdownerProvider;
        [Inject] private readonly ISceneLoader _sceneLoader;

        private void Start()
        {
            _shutdownerProvider.SetLocalShutdownProvider(new DefaultShutdowner(_sceneLoader));

            if (_runnerProvider.TryGetNetworkRunner(out var runner))
            {
                if (runner.IsServer || runner.IsShutdown) 
                    _sceneLoader.LoadTargetScene();
            }
            else
            {
                _sceneLoader.LoadTargetScene();
            }
        }
    }
}
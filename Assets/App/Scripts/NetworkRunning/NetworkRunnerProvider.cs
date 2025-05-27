using System;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace App.NetworkRunning
{
    public class NetworkRunnerProvider
    {
        private readonly NetworkRunner _networkRunnerPrefab;
        private readonly DiContainer _diContainer;

        private bool _isStartInvoked;
        private NetworkRunner _networkRunner;

        public NetworkRunnerProvider(NetworkRunner networkRunnerPrefab, 
            DiContainer diContainer)
        {
            _networkRunnerPrefab = networkRunnerPrefab;
            _diContainer = diContainer;
        }

        public Task<StartGameResult> StartGame(bool provideInput, StartGameArgs args)
        {
            if (_isStartInvoked && _networkRunner != null)
                throw new Exception("You try start game, when it started");

            _isStartInvoked = true;
            
            if (_networkRunner == null) 
                InstantiateNetworkRunner();

            _networkRunner.ProvideInput = true;
             return _networkRunner.StartGame(args);
        }
        
        public NetworkRunner GetNetworkRunner()
        {
            if (_networkRunner == null) 
                InstantiateNetworkRunner();

            return _networkRunner;
        }
        
        public bool TryGetNetworkRunner(out NetworkRunner networkRunner)
        {
            networkRunner = _networkRunner;
            return _networkRunner != null || _isStartInvoked && _networkRunner.IsShutdown;
        }

        public async void Shutdown()
        {
            await _networkRunner.Shutdown();
            await Task.Delay(100);
        }
        
        private void InstantiateNetworkRunner()
        {
            if (_networkRunner != null)
            {
                Debug.LogError("_network runner exist");
                return;
            }
            
            _isStartInvoked = false;
            _networkRunner = Object.Instantiate(_networkRunnerPrefab);
            _diContainer.InjectGameObject(_networkRunner.gameObject);
        }
    }
}
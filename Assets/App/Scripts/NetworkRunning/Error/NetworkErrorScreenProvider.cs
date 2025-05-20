using App.CursorBehaviour;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace App.NetworkRunning.Error
{
    public class NetworkErrorScreenProvider
    {
        private readonly NetworkErrorScreen _errorScreen;
        
        public NetworkErrorScreenProvider(NetworkErrorScreen prefab, CursorVisibilityBehaviour cursorVisibilityBehaviour)
        {
            _errorScreen = Object.Instantiate(prefab);
            Object.DontDestroyOnLoad(_errorScreen);
            _errorScreen.Initialize(cursorVisibilityBehaviour);
        }

        public void Show(ShutdownReason error) 
            => _errorScreen.Show(error);

        public void Show(NetDisconnectReason error)
            => _errorScreen.Show(error);
        
        public void Show(NetConnectFailedReason error)
            => _errorScreen.Show(error);
    }
}
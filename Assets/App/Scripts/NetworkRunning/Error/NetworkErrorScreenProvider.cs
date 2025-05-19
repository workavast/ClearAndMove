using App.CursorBehaviour;
using UnityEngine;

namespace App.NetworkRunning
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

        public void ShowError(string error) 
            => _errorScreen.ShowError(error);
    }
}
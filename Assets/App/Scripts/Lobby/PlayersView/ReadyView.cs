using App.Lobby.SessionData;
using UnityEngine;

namespace App.Lobby.PlayersView
{
    public class ReadyView : MonoBehaviour
    {
        [SerializeField] private ReadyMark readyMark;
        
        private NetLobbySessionData _lobbySessionData;
        
        public void Initialize(NetLobbySessionData lobbySessionData)
        {
            if (_lobbySessionData != null) 
                _lobbySessionData.OnReadyStateChanged -= UpdateReadyStateView;

            _lobbySessionData = lobbySessionData;
            _lobbySessionData.OnReadyStateChanged += UpdateReadyStateView;

            UpdateReadyStateView();
        }

        private void OnDestroy()
        {
            if (_lobbySessionData != null) 
                _lobbySessionData.OnReadyStateChanged -= UpdateReadyStateView;
        }

        private void UpdateReadyStateView() 
            => readyMark.SetState(_lobbySessionData.IsReady);
    }
}
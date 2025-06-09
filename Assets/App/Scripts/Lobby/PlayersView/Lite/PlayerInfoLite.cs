using App.Lobby.SessionData;
using App.Players.SessionData.Global;
using UnityEngine;

namespace App.Lobby.PlayersView.Lite
{
    public class PlayerInfoLite : MonoBehaviour
    {
        [SerializeField] private ReadyView readyView;
        
        private NetGlobalSessionData _globalSessionData;

        public void Initialize(NetGlobalSessionData globalSessionData, NetLobbySessionData lobbySessionData)
        {
            if (_globalSessionData != null)
            {
                _globalSessionData.OnDespawned -= DestroySelf;
            }

            _globalSessionData = globalSessionData;
            _globalSessionData.OnDespawned += DestroySelf;

            readyView.Initialize(lobbySessionData);
        }

        private void OnDestroy()
        {
            if (_globalSessionData != null) 
                _globalSessionData.OnDespawned -= DestroySelf;
        }

        private void DestroySelf() 
            => Destroy(gameObject);
    }
}
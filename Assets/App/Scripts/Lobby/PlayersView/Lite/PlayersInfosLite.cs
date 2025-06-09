using App.Lobby.SessionData;
using App.Players.SessionData.Global;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Lobby.PlayersView.Lite
{
    public class PlayersInfosLite : MonoBehaviour
    {
        [SerializeField] private PlayerInfoLite playerInfoPrefab;
        [SerializeField] private Transform holder;
        
        [Inject] private readonly LobbySessionDataRepository _lobbySessionDataRepository;
        [Inject] private readonly GlobalSessionDataRepository _globalSessionDataRepository;

        private void Awake()
        {
            _lobbySessionDataRepository.OnAdd += AddView;
        }

        private void AddView(PlayerRef playerRef, NetLobbySessionData lobbySessionData)
        {
            var globalSessionData = _globalSessionDataRepository.GetData(playerRef);

            var playerInfo = Instantiate(playerInfoPrefab, holder);
            playerInfo.Initialize(globalSessionData, lobbySessionData);
        }
    }
}
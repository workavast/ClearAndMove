using App.Armor;
using App.Lobby.SessionData;
using App.Players.Nicknames;
using App.Players.SessionData.Global;
using App.Weapons;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Lobby.PlayersView
{
    public class PlayersInfos : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfoPrefab;
        [SerializeField] private Transform holder;
        
        [Inject] private readonly LobbySessionDataRepository _lobbySessionDataRepository;
        [Inject] private readonly GlobalSessionDataRepository _globalSessionDataRepository;
        [Inject] private readonly NicknamesProvider _nicknamesProvider;
        [Inject] private readonly WeaponsConfigs _weaponsConfigs;
        [Inject] private readonly ArmorsConfig _armorsConfig;

        private void Awake()
        {
            _lobbySessionDataRepository.OnAdd += AddView;
        }

        private void AddView(PlayerRef playerRef, NetLobbySessionData lobbySessionData)
        {
            var globalSessionData = _globalSessionDataRepository.GetData(playerRef);

            var playerInfo = Instantiate(playerInfoPrefab, holder);
            playerInfo.Initialize(globalSessionData, lobbySessionData, _nicknamesProvider, _weaponsConfigs, _armorsConfig);
        }
    }
}
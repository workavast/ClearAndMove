using App.Armor;
using App.Lobby.SessionData;
using App.Players.Nicknames;
using App.Players.SessionData.Global;
using App.Weapons;
using Avastrad.Extensions;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Lobby.PlayersView
{
    public class PlayersInfos : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfoPrefab;
        [SerializeField] private Transform holder;
        [SerializeField] private Vector2 minOffset;
        [SerializeField] private Vector2 maxOffset;
        
        [Inject] private readonly LobbySessionDataRepository _lobbySessionDataRepository;
        [Inject] private readonly GlobalSessionDataRepository _globalSessionDataRepository;
        [Inject] private readonly NicknamesProvider _nicknamesProvider;
        [Inject] private readonly WeaponConfigsRep _weaponConfigsRep;
        [Inject] private readonly ArmorConfigsRep _armorConfigsRep;

        private void Awake()
        {
            _lobbySessionDataRepository.OnAdd += AddView;
        }

        private void AddView(PlayerRef playerRef, NetLobbySessionData lobbySessionData)
        {
            var globalSessionData = _globalSessionDataRepository.GetData(playerRef);

            var playerInfo = Instantiate(playerInfoPrefab, holder);
            playerInfo.Initialize(globalSessionData, lobbySessionData, _nicknamesProvider, _weaponConfigsRep, _armorConfigsRep);
        }
    }
}
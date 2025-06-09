using App.Armor;
using App.Lobby.SessionData;
using App.Players.Nicknames;
using App.Players.SessionData.Global;
using App.Weapons;
using UnityEngine;

namespace App.Lobby.PlayersView
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private NickNameView nickNameView;
        [SerializeField] private ReadyView readyView;
        [SerializeField] private SelectedWeaponView selectedWeaponView;
        [SerializeField] private SelectedArmorView selectedArmorView;
        
        private NetGlobalSessionData _globalSessionData;

        public void Initialize(NetGlobalSessionData globalSessionData, NetLobbySessionData lobbySessionData,
            NicknamesProvider nicknamesProvider, WeaponsConfigs weaponsConfigs, ArmorsConfig armorsConfig)
        {
            if (_globalSessionData != null)
            {
                _globalSessionData.OnDespawned -= DestroySelf;
            }

            _globalSessionData = globalSessionData;
            _globalSessionData.OnDespawned += DestroySelf;

            readyView.Initialize(lobbySessionData);
            selectedWeaponView.Initialize(lobbySessionData, weaponsConfigs);
            selectedArmorView.Initialize(lobbySessionData, armorsConfig);
            nickNameView.Initialize(nicknamesProvider, globalSessionData);
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
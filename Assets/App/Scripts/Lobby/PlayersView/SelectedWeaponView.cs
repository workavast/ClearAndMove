using App.Lobby.SessionData;
using App.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.PlayersView
{
    public class SelectedWeaponView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        
        private WeaponsConfigs _weaponsConfigs;
        private NetLobbySessionData _lobbySessionData;

        public void Initialize(NetLobbySessionData lobbySessionData, WeaponsConfigs weaponsConfigs)
        {
            if (_lobbySessionData != null) 
                _lobbySessionData.OnSelectedWeaponChanged -= UpdateView;

            _weaponsConfigs = weaponsConfigs;
            _lobbySessionData = lobbySessionData;
            _lobbySessionData.OnSelectedWeaponChanged += UpdateView;

            UpdateView();
        }

        private void OnDestroy()
        {
            if (_lobbySessionData != null) 
                _lobbySessionData.OnSelectedWeaponChanged -= UpdateView;
        }

        private void UpdateView() 
            => iconImage.sprite = _weaponsConfigs.GetIcon(_lobbySessionData.SelectedWeapon);
    }
}
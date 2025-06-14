using App.Lobby.SessionData;
using App.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.PlayersView
{
    public class SelectedWeaponView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        
        private WeaponConfigsRep _weaponConfigsRep;
        private NetLobbySessionData _lobbySessionData;

        public void Initialize(NetLobbySessionData lobbySessionData, WeaponConfigsRep weaponConfigsRep)
        {
            if (_lobbySessionData != null) 
                _lobbySessionData.OnSelectedWeaponChanged -= UpdateView;

            _weaponConfigsRep = weaponConfigsRep;
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
            => iconImage.sprite = _weaponConfigsRep.GetIcon(_lobbySessionData.SelectedWeapon);
    }
}
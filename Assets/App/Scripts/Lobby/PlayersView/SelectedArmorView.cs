using App.Armor;
using App.Lobby.SessionData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Lobby.PlayersView
{
    public class SelectedArmorView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        
        private ArmorConfigsRep _configs;
        private NetLobbySessionData _lobbySessionData;

        public void Initialize(NetLobbySessionData lobbySessionData, ArmorConfigsRep armorConfigsRep)
        {
            if (_lobbySessionData != null) 
                _lobbySessionData.OnSelectedArmorChanged -= UpdateView;

            _configs = armorConfigsRep;
            _lobbySessionData = lobbySessionData;
            _lobbySessionData.OnSelectedArmorChanged += UpdateView;

            UpdateView();
        }

        private void OnDestroy()
        {
            if (_lobbySessionData != null) 
                _lobbySessionData.OnSelectedArmorChanged -= UpdateView;
        }

        private void UpdateView() 
            => iconImage.sprite = _configs.GetIcon(_lobbySessionData.SelectedArmor);
    }
}
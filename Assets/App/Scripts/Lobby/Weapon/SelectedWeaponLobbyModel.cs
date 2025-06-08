using App.Players;
using App.Weapons;
using UnityEngine;

namespace App.Lobby.Weapon
{
    public class SelectedWeaponLobbyModel : MonoBehaviour
    {
        [SerializeField] private Transform holder;
        [SerializeField] private WeaponsConfigs weaponsConfigs;
        
        private void OnEnable()
        {
            PlayerData.OnWeaponChanged += UpdateData;
            UpdateData();
        }

        private void OnDisable()
        {
            PlayerData.OnWeaponChanged -= UpdateData;
        }

        private void UpdateData()
        {
            var childCount = holder.childCount;
            for (int i = 0; i < childCount; i++) 
                Destroy(holder.GetChild(i).gameObject);

            Instantiate(weaponsConfigs.WeaponConfigs[PlayerData.SelectedWeapon].LobbyPrefabVariant, holder);
        }
    }
}
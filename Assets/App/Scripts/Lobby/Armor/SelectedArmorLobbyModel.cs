using App.Armor;
using App.Players;
using UnityEngine;
using Zenject;

namespace App.Lobby.Armor
{
    public class SelectedArmorLobbyModel : MonoBehaviour
    {
        [SerializeField] private Transform holder;
        
        [Inject] private readonly ArmorConfigsRep _armorConfigsRep;
        
        private void OnEnable()
        {
            PlayerData.OnArmorLevelChanged += UpdateData;
            UpdateData();
        }

        private void OnDisable()
        {
            PlayerData.OnArmorLevelChanged -= UpdateData;
        }

        private void UpdateData()
        {
            var childCount = holder.childCount;
            for (int i = 0; i < childCount; i++) 
                Destroy(holder.GetChild(i).gameObject);

            Instantiate(_armorConfigsRep.GetArmor(PlayerData.EquippedArmorLevel).LobbyPrefabVariant, holder);
        }
    }
}
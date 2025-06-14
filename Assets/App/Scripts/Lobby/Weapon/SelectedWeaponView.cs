using App.Players;
using App.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

namespace App.Lobby.Weapon
{
    public class SelectedWeaponView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private LocalizedString localizedString;
        
        [Inject] private readonly WeaponConfigsRep _weaponConfigsRep;
        
        private void Awake()
        {
            localizedString.Arguments = new object[ ]{""};
        }
        
        private void OnEnable()
        {
            localizedString.StringChanged += UpdateLocale;
            PlayerData.OnWeaponChanged += UpdateData;
            UpdateData();
        }

        private void OnDisable()
        {
            localizedString.StringChanged -= UpdateLocale;
            PlayerData.OnWeaponChanged -= UpdateData;
        }

        private void UpdateData()
        {
            localizedString.Arguments[0] = _weaponConfigsRep.WeaponConfigs[PlayerData.SelectedWeapon].Name;
            localizedString.RefreshString();
        }
        
        private void UpdateLocale(string stringValue)
        {
            tmpText.text = stringValue;
        }
    }
}
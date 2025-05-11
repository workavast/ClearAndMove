using App.Players;
using App.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace App.UI.WeaponSelection
{
    public class SelectedWeaponView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private LocalizedString localizedString;
        [SerializeField] private WeaponsConfigs weaponsConfigs;
        
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
            localizedString.Arguments[0] = weaponsConfigs.WeaponConfigs[PlayerData.SelectedWeapon].Name;
            localizedString.RefreshString();
        }
        
        private void UpdateLocale(string stringValue)
        {
            tmpText.text = stringValue;
        }
    }
}
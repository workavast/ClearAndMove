using App.Players;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace App.UI.WeaponSelection
{
    public class SelectedWeaponView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private LocalizedString localizedString;

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
            localizedString.Arguments[0] = PlayerData.SelectedWeapon.ToString();
            localizedString.RefreshString();
        }
        
        private void UpdateLocale(string stringValue)
        {
            tmpText.text = stringValue;
        }
    }
}
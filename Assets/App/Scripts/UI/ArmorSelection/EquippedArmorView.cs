using App.Players;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace App.UI.ArmorSelection
{
    public class EquippedArmorView : MonoBehaviour
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
            PlayerData.OnArmorLevelChanged += UpdateData;
            UpdateData();
        }

        private void OnDisable()
        {
            localizedString.StringChanged -= UpdateLocale;
            PlayerData.OnArmorLevelChanged -= UpdateData;
        }

        private void UpdateData()
        {
            localizedString.Arguments[0] = PlayerData.EquippedArmorLevel.ToString();
            localizedString.RefreshString();
        }
        
        private void UpdateLocale(string stringValue)
        {
            tmpText.text = stringValue;
        }
    }
}
using App.Missions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace App.UI.MissionSelection
{
    public class SelectedMissionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameField;
        [SerializeField] private LocalizedString localizedString;

        private void Awake()
        {
            localizedString.Arguments = new object[ ]{""};
        }

        private void OnEnable()
        {
            localizedString.StringChanged += UpdateLocale;
        }

        private void OnDisable()
        {
            localizedString.StringChanged -= UpdateLocale;
        }

        public void SetData(MissionConfig missionConfig)
        {
            localizedString.Arguments[0] = missionConfig.MissionName;
            localizedString.RefreshString();
        }

        private void UpdateLocale(string stringValue)
        {
            nameField.text = stringValue;
        }
    }
}
using App.Lobby.Missions.Map;
using App.Missions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Lobby.Missions
{
    public class SelectedMissionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameField;
        [SerializeField] private LocalizedString localizedString;
        [SerializeField] private InterfaceReference<IMissionMapViewModel> mapViewModel;
        
        private void Awake()
        {
            localizedString.Arguments = new object[ ]{""};
        }

        private void OnEnable()
        {
            mapViewModel.Value.OnActiveMissionChanged += UpdateData;
            localizedString.StringChanged += UpdateLocale;

            UpdateData(mapViewModel.Value.GetActiveMission());
        }

        private void OnDisable()
        {
            mapViewModel.Value.OnActiveMissionChanged -= UpdateData;
            localizedString.StringChanged -= UpdateLocale;
        }

        private void UpdateData(MissionConfig missionConfig)
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
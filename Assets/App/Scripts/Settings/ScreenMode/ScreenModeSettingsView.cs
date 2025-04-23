using TMPro;
using UnityEngine;

namespace App.Settings.ScreenMode
{
    public class ScreenModeSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private TMP_Dropdown screenModeDropdown;
        [SerializeField] private ScreenModeSettingsViewModel viewModel;

        public void Initialize()
        {
            UpdateView();
        }

        private void OnEnable()
        {
            viewModel.OnChanged += UpdateView;
            screenModeDropdown.onValueChanged.AddListener(Set);
            UpdateView();
        }

        private void OnDisable()
        {
            viewModel.OnChanged -= UpdateView;
            screenModeDropdown.onValueChanged.RemoveListener(Set);
        }

        private void Set(int value)
        {
            viewModel.Set(screenModeDropdown.value == 1, false);
        }

        private void UpdateView()
        {
            screenModeDropdown.SetValueWithoutNotify(viewModel.IsFullScreen ? 1 : 0);
            screenModeDropdown.RefreshShownValue();
        }
    }
}
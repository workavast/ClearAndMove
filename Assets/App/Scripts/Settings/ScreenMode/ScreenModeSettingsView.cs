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

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            screenModeDropdown.onValueChanged.AddListener(Set);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            screenModeDropdown.onValueChanged.RemoveListener(Set);
        }

        private void Set(int value)
        {
            viewModel.Set(screenModeDropdown.value == 0, false);
        }

        private void UpdateView()
        {
            screenModeDropdown.SetValueWithoutNotify(viewModel.IsFullScreen ? 0 : 1);
            screenModeDropdown.RefreshShownValue();
        }
    }
}
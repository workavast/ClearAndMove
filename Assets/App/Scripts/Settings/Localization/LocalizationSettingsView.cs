using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Settings.Localization
{
    public class LocalizationSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private TMP_Dropdown fpsDropdown;
        [SerializeField] private LocalizationSettingsViewModel viewModel;

        public IReadOnlyList<Locale> Options => viewModel.Options;
        private int SelectedOptionIndex => viewModel.SelectedOptionIndex;

        public void Initialize()
        {
            fpsDropdown.ClearOptions();
            foreach (var option in Options)
                fpsDropdown.options.Add(new TMP_Dropdown.OptionData(option.LocaleName));
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            fpsDropdown.onValueChanged.AddListener(SetValue);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            fpsDropdown.onValueChanged.RemoveListener(SetValue);
        }

        private void UpdateView()
        {
            fpsDropdown.SetValueWithoutNotify(SelectedOptionIndex);
            fpsDropdown.RefreshShownValue();
        }

        private void SetValue(int optionIndex)
            => viewModel.Set(optionIndex, false);
    }
}
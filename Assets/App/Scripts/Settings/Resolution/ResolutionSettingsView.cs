using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.Settings.Resolution
{
    public class ResolutionSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private ResolutionSettingsViewModel viewModel;

        private IReadOnlyList<InspectorResolution> Resolutions => viewModel.Resolutions;
        private int SelectedResolutionIndex => viewModel.SelectedResolutionIndex;

        public void Initialize()
        {
            resolutionDropdown.ClearOptions();

            var currentResolutionIndex = 0;
            for (var i = 0; i < Resolutions.Count; i++)
            {
                var resolution = Resolutions[i];
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.Width + " x " + resolution.Height));

                if (resolution.Width == Screen.currentResolution.width && resolution.Height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
            }

            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            
            UpdateView();
        }

        private void OnEnable()
        {
            viewModel.OnChanged += UpdateView;
            resolutionDropdown.onValueChanged.AddListener(Set);
            UpdateView();
        }

        private void OnDisable()
        {
            viewModel.OnChanged -= UpdateView;
            resolutionDropdown.onValueChanged.RemoveListener(Set);
        }

        private void Set(int resolutionIndex) 
            => viewModel.Set(resolutionIndex, false);

        private void UpdateView()
        {
            resolutionDropdown.SetValueWithoutNotify(SelectedResolutionIndex);
            resolutionDropdown.RefreshShownValue();
        }
    }
}
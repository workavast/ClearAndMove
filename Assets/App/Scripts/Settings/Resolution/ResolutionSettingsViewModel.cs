using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace App.Settings.Resolution
{
    public class ResolutionSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;
        
        public int SelectedResolutionIndex { get; private set; }
        public IReadOnlyList<InspectorResolution> Resolutions => Model.Resolutions;
        public bool IsChanged => SelectedResolutionIndex != Model.SelectedResolutionIndex;
        
        private ResolutionSettingsModel Model => _settingsModel.ResolutionSettingsModel;
        private int DefaultResolutionIndex => Model.DefaultResolutionIndex;

        public event Action OnChanged;

        public void Initialize()
        {
            SelectedResolutionIndex = Model.SelectedResolutionIndex;
        }

        public void ApplySettings()
        {
            if (!IsChanged)
                return;
            
            Model.Set(SelectedResolutionIndex);
        }

        public void Set(int resolutionIndex, bool notify)
        {
            if (SelectedResolutionIndex == resolutionIndex)
                return;

            SelectedResolutionIndex = resolutionIndex;
            if (notify)
                OnChanged?.Invoke();
        }
        
        public void ResetSettings()
        {
            Set(Model.SelectedResolutionIndex, true);
        }

        public void ResetToDefault()
        {
            Set(DefaultResolutionIndex, true);
            ApplySettings();
        }
    }
}
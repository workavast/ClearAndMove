using App.Audio;
using UnityEngine;
using Zenject;

namespace App.Settings
{
    public class SettingsPresenter : MonoBehaviour
    {
        [SerializeField] private ResolutionSettingsController resolutionSettingsController;

        [Inject] private readonly SettingsModel _settingsModel;
        [Inject] private readonly AudioVolumeChanger _audioVolumeChanger;

        private ISettingsViewModel[] _settingsViewModels;
        private ISettingsView[] _settingsViews;

        private void Awake()
        {
            _settingsViewModels = GetComponentsInChildren<ISettingsViewModel>(true);
            _settingsViews = GetComponentsInChildren<ISettingsView>(true);

            foreach (var viewModel in _settingsViewModels)
                viewModel.Initialize();

            foreach (var view in _settingsViews)
                view.Initialize();
        }

        public void ApplySettings()
        {
            _audioVolumeChanger.Apply();
            resolutionSettingsController.Apply();

            foreach (var viewModel in _settingsViewModels)
                viewModel.ApplySettings();

            _settingsModel.Save();
        }

        public void ResetSettings()
        {
            foreach (var viewModel in _settingsViewModels)
                viewModel.ResetSettings();

            _audioVolumeChanger.ResetToDefault();
            resolutionSettingsController.ResetToDefault();

            _settingsModel.Save();
        }

        public void ResetToDefaults()
        {
            foreach (var viewModel in _settingsViewModels)
                viewModel.ResetToDefault();

            _settingsModel.Save();
        }
    }
}

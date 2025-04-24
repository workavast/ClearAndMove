using UnityEngine;
using Zenject;

namespace App.Settings
{
    public class SettingsPresenter : MonoBehaviour
    {
        [Inject] private readonly SettingsModel _settingsModel;

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
            foreach (var viewModel in _settingsViewModels)
                viewModel.ApplySettings();

            _settingsModel.Save();
        }

        public void ResetSettings()
        {
            foreach (var viewModel in _settingsViewModels)
                viewModel.ResetSettings();

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

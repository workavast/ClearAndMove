using System;
using App.Settings.Fps;
using App.Settings.ScreenMode;
using UnityEngine;
using Zenject;

namespace App.Settings
{
    [Serializable]
    public class SettingsModel
    {
        [field: SerializeField] public FpsSettingsModel FpsSettingsModel { get; private set; }
        [field: SerializeField] public ScreenModeSettingsModel ScreenModeSettingsModel { get; private set; }

        private readonly ISettingsModel[] _allSettings;

        [Inject]
        public SettingsModel(FpsConfig fpsConfig, ScreenModeConfig screenModeConfig)
        {
            FpsSettingsModel = new FpsSettingsModel(fpsConfig);
            ScreenModeSettingsModel = new ScreenModeSettingsModel(screenModeConfig);

            _allSettings = new ISettingsModel[]
            {
                FpsSettingsModel,
                ScreenModeSettingsModel
            };
        }

        public void Load(SettingsModel model)
        {
            FpsSettingsModel.Load(model.FpsSettingsModel);
            ScreenModeSettingsModel.Load(model.ScreenModeSettingsModel);
        }

        public void Save()
            => SettingsSaver.Save(this);

        public void Apply()
        {
            foreach (var settings in _allSettings)
                settings.Apply();
        }
    }
}
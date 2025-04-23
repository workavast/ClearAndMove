using System;
using App.Settings.Fps;
using App.Settings.Resolution;
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
        [field: SerializeField] public ResolutionSettingsModel ResolutionSettingsModel { get; private set; }

        private readonly ISettingsModel[] _allSettings;

        [Inject]
        public SettingsModel(FpsConfig fpsConfig, ScreenModeConfig screenModeConfig, ResolutionSettingsConfig resolutionConfig)
        {
            FpsSettingsModel = new FpsSettingsModel(fpsConfig);
            ScreenModeSettingsModel = new ScreenModeSettingsModel(screenModeConfig);
            ResolutionSettingsModel = new ResolutionSettingsModel(resolutionConfig);

            _allSettings = new ISettingsModel[]
            {
                FpsSettingsModel,
                ScreenModeSettingsModel,
                ResolutionSettingsModel
            };
        }

        public void Load(SettingsModel model)
        {
            FpsSettingsModel.Load(model.FpsSettingsModel);
            ScreenModeSettingsModel.Load(model.ScreenModeSettingsModel);
            ResolutionSettingsModel.Load(model.ResolutionSettingsModel);
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
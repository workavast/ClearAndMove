using System;
using App.Settings.Fps;
using App.Settings.Localization;
using App.Settings.Resolution;
using App.Settings.ScreenMode;
using App.Settings.Volume;
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
        [field: SerializeField] public VolumeSettingsModel VolumeSettingsModel { get; private set; }
        [field: SerializeField] public LocalizationSettingsModel LocalizationSettingsModel { get; private set; }

        private readonly ISettingsModel[] _allSettings;

        [Inject]
        public SettingsModel(FpsConfig fpsConfig, ScreenModeConfig screenModeConfig, 
            ResolutionSettingsConfig resolutionConfig, VolumeSettingsConfig volumeConfig, 
            LocalizationConfig localizationConfig)
        {
            FpsSettingsModel = new FpsSettingsModel(fpsConfig);
            ScreenModeSettingsModel = new ScreenModeSettingsModel(screenModeConfig);
            ResolutionSettingsModel = new ResolutionSettingsModel(resolutionConfig);
            VolumeSettingsModel = new VolumeSettingsModel(volumeConfig);
            LocalizationSettingsModel = new LocalizationSettingsModel(localizationConfig);

            _allSettings = new ISettingsModel[]
            {
                FpsSettingsModel,
                ScreenModeSettingsModel,
                ResolutionSettingsModel,
                VolumeSettingsModel,
                LocalizationSettingsModel
            };
            Array.Sort(_allSettings, (x, y) => -x.Priority.CompareTo(y.Priority));
        }

        public void Load(SettingsModel model)
        {
            FpsSettingsModel.Load(model.FpsSettingsModel);
            ScreenModeSettingsModel.Load(model.ScreenModeSettingsModel);
            ResolutionSettingsModel.Load(model.ResolutionSettingsModel);
            VolumeSettingsModel.Load(model.VolumeSettingsModel);
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
using System;
using App.Settings.CameraShakePower;
using App.Settings.Fps;
using App.Settings.Localization;
using App.Settings.Resolution;
using App.Settings.ScreenMode;
using App.Settings.Volume;
using App.Settings.VSync;
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
        [field: SerializeField] public CameraShakePowerSettingsModel CameraShakePowerSettingsModel { get; private set; }
        [field: SerializeField] public VSyncSettingsModel VSyncSettingsModel { get; private set; }

        private readonly ISettingsModel[] _allSettings;

        [Inject]
        public SettingsModel(SettingsConfigsRepository configsRepository)
        {
            FpsSettingsModel = new FpsSettingsModel(configsRepository.GetConfig<FpsConfig>());
            ScreenModeSettingsModel = new ScreenModeSettingsModel(configsRepository.GetConfig<ScreenModeConfig>());
            ResolutionSettingsModel = new ResolutionSettingsModel(configsRepository.GetConfig<ResolutionSettingsConfig>());
            VolumeSettingsModel = new VolumeSettingsModel(configsRepository.GetConfig<VolumeSettingsConfig>());
            LocalizationSettingsModel = new LocalizationSettingsModel(configsRepository.GetConfig<LocalizationConfig>());
            CameraShakePowerSettingsModel = new CameraShakePowerSettingsModel(configsRepository.GetConfig<CameraShakePowerSettingsConfig>());
            VSyncSettingsModel = new VSyncSettingsModel(configsRepository.GetConfig<VSyncSettingsConfig>());
            
            _allSettings = new ISettingsModel[]
            {
                FpsSettingsModel,
                ScreenModeSettingsModel,
                ResolutionSettingsModel,
                VolumeSettingsModel,
                LocalizationSettingsModel,
                CameraShakePowerSettingsModel,
                VSyncSettingsModel
            };
            Array.Sort(_allSettings, (x, y) => -x.Priority.CompareTo(y.Priority));
        }

        public void Load(SettingsModel model)
        {
            FpsSettingsModel.Load(model.FpsSettingsModel);
            ScreenModeSettingsModel.Load(model.ScreenModeSettingsModel);
            ResolutionSettingsModel.Load(model.ResolutionSettingsModel);
            VolumeSettingsModel.Load(model.VolumeSettingsModel);
            LocalizationSettingsModel.Load(model.LocalizationSettingsModel);
            CameraShakePowerSettingsModel.Load(model.CameraShakePowerSettingsModel);
            VSyncSettingsModel.Load(model.VSyncSettingsModel);
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
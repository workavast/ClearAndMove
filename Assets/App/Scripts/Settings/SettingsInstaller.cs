using App.Settings.Fps;
using App.Settings.Localization;
using App.Settings.Resolution;
using App.Settings.ScreenMode;
using App.Settings.Volume;
using UnityEngine;
using Zenject;

namespace App.Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private FpsConfig fpsConfig;
        [SerializeField] private ScreenModeConfig screenModeConfig;
        [SerializeField] private ResolutionSettingsConfig resolutionConfig;
        [SerializeField] private VolumeSettingsConfig volumeConfig;
        [SerializeField] private LocalizationConfig localizationConfig;

        public override void InstallBindings()
        {
            var settingsModel = new SettingsModel(fpsConfig, screenModeConfig, resolutionConfig, volumeConfig, localizationConfig);
            Container.Bind<SettingsModel>().FromInstance(settingsModel).AsSingle();
        }
    }
}
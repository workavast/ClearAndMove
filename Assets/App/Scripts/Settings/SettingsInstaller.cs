using App.Settings.Fps;
using App.Settings.Resolution;
using App.Settings.ScreenMode;
using UnityEngine;
using Zenject;

namespace App.Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private FpsConfig fpsConfig;
        [SerializeField] private ScreenModeConfig screenModeConfig;
        [SerializeField] private ResolutionSettingsConfig resolutionConfig;
        
        public override void InstallBindings()
        {
            var settingsModel = new SettingsModel(fpsConfig, screenModeConfig, resolutionConfig);
            Container.Bind<SettingsModel>().FromInstance(settingsModel).AsSingle();
        }
    }
}
using App.Settings.Fps;
using App.Settings.ScreenMode;
using UnityEngine;
using Zenject;

namespace App.Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private FpsConfig fpsConfig;
        [SerializeField] private ScreenModeConfig screenModeConfig;
        
        public override void InstallBindings()
        {
            var settingsModel = new SettingsModel(fpsConfig, screenModeConfig);
            Container.Bind<SettingsModel>().FromInstance(settingsModel).AsSingle();
        }
    }
}
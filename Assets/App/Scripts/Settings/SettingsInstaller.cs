using UnityEngine;
using Zenject;

namespace App.Settings
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private SettingsConfigsRepository settingsConfigsRepository;

        public override void InstallBindings()
        {
            var settingsModel = new SettingsModel(settingsConfigsRepository);
            Container.Bind<SettingsModel>().FromInstance(settingsModel).AsSingle();
        }
    }
}
using App.Audio.Ambience;
using UnityEngine;
using Zenject;

namespace App.Audio
{
    public class AudioProjectInstaller : MonoInstaller
    {
        [SerializeField] private AmbienceConfig ambienceConfig;
        
        public override void InstallBindings()
        {
            BindAmbience();
        }

        private void BindAmbience()
        {
            Container.Bind<AmbienceConfig>().FromInstance(ambienceConfig).AsSingle();
            
            var ambienceManagerHolder = new GameObject() { name =  nameof(AmbienceManager)};
            var ambienceManager = ambienceManagerHolder.AddComponent<AmbienceManager>();

            Container.Bind<AmbienceManager>().FromInstance(ambienceManager).AsSingle();
        }
    }
}
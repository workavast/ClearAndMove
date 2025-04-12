using FOVMapping;
using UnityEngine;
using Zenject;

namespace App.WarFog
{
    public class FOVInstaller : MonoInstaller
    {
        [SerializeField] private FOVManager fovManager;
        
        public override void InstallBindings()
        {
            Container.Bind<FOVManagerProvider>().FromNew().AsSingle().WithArguments(fovManager).NonLazy();
        }
    }
}
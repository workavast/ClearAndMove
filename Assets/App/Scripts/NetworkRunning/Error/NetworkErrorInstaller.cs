using UnityEngine;
using Zenject;

namespace App.NetworkRunning.Error
{
    public class NetworkErrorInstaller : MonoInstaller
    {
        [SerializeField] private NetworkErrorScreen networkErrorScreenPrefab;

        public override void InstallBindings()
        {
            Container.Bind<NetworkErrorScreenProvider>().FromNew().AsSingle().WithArguments(networkErrorScreenPrefab).NonLazy();
        }
    }
}
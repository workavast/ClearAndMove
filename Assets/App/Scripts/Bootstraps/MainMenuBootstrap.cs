using App.Localization;
using App.NetworkRunning.Shutdowners;
using App.NetworkRunning.Shutdowners.LocalShutdowners;
using Avastrad.ScenesLoading;
using UnityEngine;
using Zenject;

namespace App.Bootstraps
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private StringTablesPreloader localizationPreloader;

        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private readonly ShutdownerProvider _shutdownerProvider;

        private async void Start()
        {
            _shutdownerProvider.SetLocalShutdownProvider(new DefaultShutdowner(_sceneLoader));
            await localizationPreloader.Preload();

            _sceneLoader.HideLoadScreen(false);
        }

        private void OnDestroy() 
            => localizationPreloader.Release();
    }
}
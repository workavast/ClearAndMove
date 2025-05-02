using App.CursorBehaviour;
using App.NetworkRunning.Shutdowners;
using App.NetworkRunning.Shutdowners.LocalShutdowners;
using App.Settings;
using Avastrad.ScenesLoading;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

namespace App.Bootstraps
{
    public class AppBootstrap : MonoBehaviour
    {
        [SerializeField] private int sceneIndexForLoadingAfterInitializations = 2;
        
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly CursorVisibilityBehaviour _cursorVisibilityBehaviour;
        [Inject] private readonly ShutdownerProvider _shutdownerProvider;
        [Inject] private readonly SettingsModel _settingsModel;
        
        private async void Start()
        {
            _cursorVisibilityBehaviour.CheckCursorVisibilityState();
            _shutdownerProvider.SetLocalShutdownProvider(new DefaultShutdowner(_sceneLoader));

            await LocalizationSettings.InitializationOperation.Task;
            
            if (SettingsSaver.Exist()) 
                _settingsModel.Load(SettingsSaver.Load());
            _settingsModel.Apply();
            
            _sceneLoader.LoadScene(sceneIndexForLoadingAfterInitializations, true); 
        }
    }
}
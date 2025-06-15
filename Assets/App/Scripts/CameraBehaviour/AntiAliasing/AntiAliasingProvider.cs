using System;
using App.Settings;
using App.Settings.AntiAliasing;
using UnityEngine.Rendering.Universal;

namespace App.CameraBehaviour.AntiAliasing
{
    public class AntiAliasingProvider : IDisposable
    {
        public AntialiasingMode AntialiasingMode { get; private set; }

        private readonly AntiAliasingSettingsModel _antiAliasingSettingsModel;
        
        public event Action OnAntialiasingModeChanged;

        private AntiAliasingProvider(SettingsModel settingsModel)
        {
            _antiAliasingSettingsModel = settingsModel.AntiAliasingSettingsModel;
            _antiAliasingSettingsModel.OnAntialiasingModeChanged += SetAntialiasingMode;
        }

        private void SetAntialiasingMode() 
            => SetAntialiasingMode(_antiAliasingSettingsModel.SelectedAntialiasingMode);

        private void SetAntialiasingMode(AntialiasingMode antialiasingMode)
        {
            AntialiasingMode = antialiasingMode;
            OnAntialiasingModeChanged?.Invoke();
        }

        public void Dispose()
        {
            if (_antiAliasingSettingsModel != null)
                _antiAliasingSettingsModel.OnAntialiasingModeChanged -= SetAntialiasingMode;
        }
    }
}
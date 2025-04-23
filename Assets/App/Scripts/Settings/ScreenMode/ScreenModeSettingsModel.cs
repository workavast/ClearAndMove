using System;
using UnityEngine;

namespace App.Settings.ScreenMode
{
    [Serializable]
    public class ScreenModeSettingsModel : ISettingsModel
    {
        [field: SerializeField] public bool IsFullScreen { get; private set; }

        public bool DefaultIsFullScreen => _config.DefaultIsFullScreen;

        private readonly ScreenModeConfig _config;

        public ScreenModeSettingsModel(ScreenModeConfig config)
        {
            _config = config;
            IsFullScreen = DefaultIsFullScreen;
        }

        public void Load(ScreenModeSettingsModel model)
        {
            IsFullScreen = model.IsFullScreen;
        }

        public void Set(bool isFullScreen)
        {
            IsFullScreen = isFullScreen;
            Apply();
        }

        public void Apply()
        {
            Screen.fullScreen = IsFullScreen;
        }

        public void ResetToDefault()
            => Set(DefaultIsFullScreen);
    }
}
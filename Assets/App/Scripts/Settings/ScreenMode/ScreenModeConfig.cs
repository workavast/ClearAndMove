using UnityEngine;

namespace App.Settings.ScreenMode
{
    [CreateAssetMenu(fileName = nameof(ScreenModeConfig), menuName = Consts.ConfigsPath + "Settings/" + nameof(ScreenModeConfig))]
    public class ScreenModeConfig : SettingsConfig
    {
        [Header("Should be have priority less then resolution to worked")]
        [Space]
        [SerializeField] private bool defaultIsFullScreen;

        public bool DefaultIsFullScreen => defaultIsFullScreen;
    }
}
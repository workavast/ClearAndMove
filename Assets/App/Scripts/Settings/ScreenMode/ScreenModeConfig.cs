using UnityEngine;

namespace App.Settings.ScreenMode
{
    [CreateAssetMenu(fileName = nameof(ScreenModeConfig), menuName = Consts.ConfigsPath + "Settings/" + nameof(ScreenModeConfig))]
    public class ScreenModeConfig : ScriptableObject
    {
        [SerializeField] private bool defaultIsFullScreen;

        public bool DefaultIsFullScreen => defaultIsFullScreen;
    }
}
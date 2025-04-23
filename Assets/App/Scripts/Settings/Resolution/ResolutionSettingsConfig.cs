using System.Collections.Generic;
using UnityEngine;

namespace App.Settings.Resolution
{
    [CreateAssetMenu(fileName = nameof(ResolutionSettingsConfig), menuName = Consts.ConfigsPath + "/Settings/" + nameof(ResolutionSettingsConfig))]
    public class ResolutionSettingsConfig : ScriptableObject
    {
        [SerializeField] private List<InspectorResolution> resolutions;
        
        public IReadOnlyList<InspectorResolution> Resolutions => resolutions;
    }
}
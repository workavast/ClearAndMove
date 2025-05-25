using UnityEngine;

namespace App.StairsZone
{
    [CreateAssetMenu(fileName = nameof(StairsZoneConfig), menuName = Consts.AppName + "/Configs/" + nameof(StairsZoneConfig))]
    public class StairsZoneConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float ExtractionTime { get; private set; } = 5f;
    }
}
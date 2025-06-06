using UnityEngine;

namespace App.WarFog
{
    [CreateAssetMenu(fileName = nameof(DynamicFieldOfViewConfig),
        menuName = Consts.AppName + "/Configs/" + nameof(DynamicFieldOfViewConfig))]
    public class DynamicFieldOfViewConfig : ScriptableObject
    {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField, Min(0)] public int RaysPerAngle { get; private set; } = 4;
        [field: SerializeField, Range(0, 360)] public float FOV { get; private set; } = 90f;
        [field: SerializeField, Min(0)] public float ViewDistance { get; private set; } = 50f;
    }
}
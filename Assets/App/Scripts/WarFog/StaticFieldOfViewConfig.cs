using UnityEngine;

namespace App.WarFog
{
    [CreateAssetMenu(fileName = nameof(StaticFieldOfViewConfig), menuName = Consts.ConfigsPath + nameof(StaticFieldOfViewConfig))]
    public class StaticFieldOfViewConfig : ScriptableObject
    {
        [field: SerializeField] public float Offset { get; private set; }
    }
}
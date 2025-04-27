using UnityEngine;

namespace App.Dissolving
{
    [CreateAssetMenu(fileName = nameof(DissolveConfig), menuName = Consts.ConfigsPath + nameof(DissolveConfig))]
    public class DissolveConfig : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
    }
}
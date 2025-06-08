using UnityEngine;

namespace App.Armor
{
    [CreateAssetMenu(fileName = nameof(ArmorConfig), menuName = Consts.AppName + "/Configs/Armor/" + nameof(ArmorConfig))]
    public class ArmorConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject LobbyPrefabVariant { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, Range(0, 1)] public float DamageScale { get; private set; } = 1f;
        [field: SerializeField, Min(0)] public float WalkSpeedDecrease { get; private set; } = 0f;
        [field: SerializeField, Min(0)] public float SprintSpeedDecrease { get; private set; } = 0f;
        [field: SerializeField, Min(0)] public float MoveAccelerationDecrease { get; private set; } = 0f;        
    }
}
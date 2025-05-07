using UnityEngine;

namespace App.Lobby.Weapon
{
    [CreateAssetMenu(fileName = nameof(WeaponParamsViewConfig), menuName = Consts.WeaponConfigsPath + nameof(WeaponParamsViewConfig))]
    public class WeaponParamsViewConfig : ScriptableObject
    {
        [field: SerializeField] public float MinDamage { get; private set; }
        [field: SerializeField] public float MaxDamage { get; private set; }
        [field: Space]
        [field: SerializeField] public float MinFireRate { get; private set; }
        [field: SerializeField] public float MaxFireRate { get; private set; }
        [field: Space]
        [field: SerializeField] public float MinReloadTime { get; private set; }
        [field: SerializeField] public float MaxReloadTime { get; private set; }
        [field: Space]
        [field: SerializeField] public float MinSpreadAngle { get; private set; }
        [field: SerializeField] public float MaxSpreadAngle { get; private set; }
        [field: Space]
        [field: SerializeField] public float MinMagazineSize { get; private set; }
        [field: SerializeField] public float MaxMagazineSize { get; private set; }
    }
}
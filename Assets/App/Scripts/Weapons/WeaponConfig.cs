using UnityEngine;

namespace App.Weapons
{
    [CreateAssetMenu(fileName = nameof(WeaponConfig), menuName = Consts.WeaponConfigsPath + nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponId Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, Min(0)] public int BulletsPerShoot { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float DamagePerBullet { get; private set; } = 10;
        [field: SerializeField, Min(0)] public float FireRate { get; private set; } = 0.2f;
        [field: Space]
        [field: SerializeField, Min(0)] public int MagazineSize { get; private set; }
        [field: SerializeField, Min(0)] public int NetFullAmmoSize { get; private set; }
        [field: SerializeField, Min(0)] public float ReloadTime { get; private set; } = 3;
        [field: Header("Spread")]
        [field: SerializeField, Range(0, 90)] public float MinSpreadAngle { get; private set; } = 10f;
        [field: SerializeField, Range(0, 90)] public float MaxSpreadAngle { get; private set; } = 10f;
        [field: SerializeField, Range(0, 90)] public float SpreadRatePerShot { get; private set; } = 0.1f;
        [field: SerializeField, Range(0, 90)] public float SpreadRateDecreasePerSecond { get; private set; } = 0.1f;

    }
}
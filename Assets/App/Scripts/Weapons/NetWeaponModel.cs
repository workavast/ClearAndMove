using System;
using App.Weapons.Shooting;
using Fusion;
using UnityEngine;

namespace App.Weapons
{
    public class NetWeaponModel : NetworkBehaviour
    {
        [field: SerializeField] public LayerMask HitLayers { get; set; }

        [OnChangedRender(nameof(OnNetEquippedWeaponChanged))]
        [Networked] [field: SerializeField, ReadOnly] private WeaponId NetEquippedWeapon { get; set; } = WeaponId.Scar;
        [Networked] [field: SerializeField, ReadOnly] public int NetMagazine { get; set; }
        [Networked] [field: SerializeField, ReadOnly] public int NetFullAmmoSize { get; set; }
        [Networked] [field: SerializeField, ReadOnly] public int NetFireCount { get; set; }
        [Networked] [field: SerializeField, ReadOnly] public float CurrentSpreadRatio { get; set; }

        [Networked] public TickTimer NetFireRatePause { get; set; }
        [Networked] public TickTimer NetReloadTimer { get; set; }
        [Networked, Capacity(32)] public NetworkArray<ProjectileData> NetProjectileData { get; }

        [field: SerializeField, ReadOnly] public WeaponConfig WeaponConfig { get; set; }

        public WeaponId EquippedWeapon => NetEquippedWeapon;
        
        public Shooter Shooter { get; set; }
        
        public event Action<WeaponId> OnEquippedWeaponChanged;

        public override void FixedUpdateNetwork()
        {
            if (CurrentSpreadRatio <= 0)
                return;

            var spreadChangeRate = WeaponConfig.SpreadRateDecreasePerSecond * Runner.DeltaTime;
            CurrentSpreadRatio = Mathf.Clamp(CurrentSpreadRatio - spreadChangeRate, 0, 1);
        }

        public void SetWeapon(WeaponId weaponId)
        {
            if (NetEquippedWeapon == weaponId)
            {
                Debug.LogWarning($"You try set weapon that already setted: [{NetEquippedWeapon}] [{weaponId}]");
                return;
            }
            
            NetEquippedWeapon = weaponId;
        }

        private void OnNetEquippedWeaponChanged() 
            => OnEquippedWeaponChanged?.Invoke(NetEquippedWeapon);
    }
}
using App.Players;
using App.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.Weapon
{
    public class WeaponParamsView : MonoBehaviour
    {
        [SerializeField] private WeaponParamsViewConfig weaponParamsViewConfig;
        [SerializeField] private WeaponsConfigs weaponsConfigs;
        [Space]
        [SerializeField] private Slider damageView;
        [SerializeField] private Slider fireRateView;
        [SerializeField] private Slider reloadTimeView;
        [SerializeField] private Slider spreadAngleView;
        [SerializeField] private Slider magazineSizeView;
        
        private void Awake()
        {
            PlayerData.OnWeaponChanged += UpdateView;

            damageView.minValue = weaponParamsViewConfig.MinDamage;
            damageView.maxValue = weaponParamsViewConfig.MaxDamage;
            
            fireRateView.minValue = weaponParamsViewConfig.MinFireRate;
            fireRateView.maxValue = weaponParamsViewConfig.MaxFireRate;
            
            reloadTimeView.minValue = weaponParamsViewConfig.MinReloadTime;
            reloadTimeView.maxValue = weaponParamsViewConfig.MaxReloadTime;
            
            spreadAngleView.minValue = weaponParamsViewConfig.MinSpreadAngle;
            spreadAngleView.maxValue = weaponParamsViewConfig.MaxSpreadAngle;

            magazineSizeView.minValue = weaponParamsViewConfig.MinMagazineSize;
            magazineSizeView.maxValue = weaponParamsViewConfig.MaxMagazineSize;
            
            UpdateView();
        }

        private void OnDestroy()
        {
            PlayerData.OnWeaponChanged -= UpdateView;
        }

        private void UpdateView()
        {
            var weaponConfig = weaponsConfigs.WeaponConfigs[PlayerData.SelectedWeapon];
            damageView.value = weaponConfig.DamagePerBullet;
            fireRateView.value = weaponConfig.FireRate;
            reloadTimeView.value = weaponConfig.ReloadTime;
            spreadAngleView.value = weaponConfig.SpreadAngle;
            magazineSizeView.value = weaponConfig.MagazineSize;
        }
    }
}
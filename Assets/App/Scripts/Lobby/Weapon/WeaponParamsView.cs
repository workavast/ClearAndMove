using App.Players;
using App.Weapons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Lobby.Weapon
{
    public class WeaponParamsView : MonoBehaviour
    {
        [SerializeField] private WeaponParamsViewConfig weaponParamsViewConfig;
        [Space]
        [SerializeField] private Slider damageView;
        [SerializeField] private Slider fireRateView;
        [SerializeField] private Slider reloadTimeView;
        [SerializeField] private Slider spreadAngleView;
        [SerializeField] private Slider magazineSizeView;
        
        [Inject] private readonly WeaponConfigsRep _weaponConfigsRep;
        
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
            var weaponConfig = _weaponConfigsRep.WeaponConfigs[PlayerData.SelectedWeapon];
            damageView.value = weaponConfig.DamagePerBullet;
            fireRateView.value = weaponConfig.FireRate;
            reloadTimeView.value = weaponConfig.ReloadTime;
            spreadAngleView.value = weaponConfig.MinSpreadAngle;
            magazineSizeView.value = weaponConfig.MagazineSize;
        }
    }
}
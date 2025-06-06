using System.Collections.Generic;
using App.UI.Selection;
using App.Weapons;
using UnityEngine;
using Zenject;

namespace App.UI.WeaponSelection
{
    public class WeaponSelector : Selector<WeaponId>
    {
        [SerializeField] private WeaponsConfigs weaponsConfigs;
        
        [Inject] private readonly Weapons.WeaponSelector _weaponSelector;

        protected override void Initialize()
        {
            weaponsConfigs.Initialize(true);
            base.Initialize();
        }

        protected override IReadOnlyList<WeaponId> GetIds() 
            => weaponsConfigs.WeaponIds;

        protected override string GetName(WeaponId id) 
            => weaponsConfigs.WeaponConfigs[id].Name;

        protected override void Select(WeaponId id)
            => _weaponSelector.SelectWeapon(id);
    }
}
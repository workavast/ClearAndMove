using System.Collections.Generic;
using App.UI.Selection;
using App.Weapons;
using UnityEngine;
using Zenject;

namespace App.Lobby.Weapon
{
    public class WeaponSelector : Selector<WeaponId, WeaponSelectBtn>
    {
        [SerializeField] private WeaponConfigsRep selectableWeaponConfigsRep;
        
        [Inject] private readonly Weapons.WeaponSelector _weaponSelector;

        protected override void Initialize()
        {
            selectableWeaponConfigsRep.Initialize(true);
            base.Initialize();
        }

        protected override IReadOnlyList<WeaponId> GetIds() 
            => selectableWeaponConfigsRep.WeaponIds;

        protected override string GetName(WeaponId id) 
            => selectableWeaponConfigsRep.WeaponConfigs[id].Name;

        protected override bool Is(WeaponId a, WeaponId b) 
            => a == b;

        protected override WeaponId GetCurrentActiveId() 
            => _weaponSelector.GetSelectedWeapon();

        protected override void OnSelect(WeaponId id)
        {
            _weaponSelector.SelectWeapon(id);
        }

        protected override void UpdateBtn(WeaponId weaponId, WeaponSelectBtn button)
        {
            button.SetData(weaponId, GetName(weaponId));
            button.SetIcon(selectableWeaponConfigsRep.WeaponConfigs[weaponId].Icon);
        }
    }
}
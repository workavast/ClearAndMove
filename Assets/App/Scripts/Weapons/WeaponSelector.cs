using System.Collections.Generic;
using App.Players;
using Zenject;

namespace App.Weapons
{
    public class WeaponSelector
    {
        [Inject] private readonly WeaponConfigsRep _weaponConfigsRep;

        public WeaponId GetSelectedWeapon() 
            => PlayerData.SelectedWeapon;

        public void SelectWeapon(WeaponId weaponId) 
            => PlayerData.SetWeapon(weaponId);

        public IReadOnlyList<WeaponId> GetAllWeapon() 
            => _weaponConfigsRep.WeaponIds;
    }
}
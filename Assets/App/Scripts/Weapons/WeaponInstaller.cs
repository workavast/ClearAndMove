using UnityEngine;
using Zenject;

namespace App.Weapons
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private WeaponConfigsRep weaponConfigsRep;
        
        public override void InstallBindings()
        {
            weaponConfigsRep.Initialize(true);
            Container.BindInstance(weaponConfigsRep).AsSingle();
            
            Container.Bind<WeaponSelector>().FromNew().AsSingle();
        }
    }
}
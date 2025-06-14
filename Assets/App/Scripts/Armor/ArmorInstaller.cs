using UnityEngine;
using Zenject;

namespace App.Armor
{
    public class ArmorInstaller : MonoInstaller
    {
        [SerializeField] private ArmorConfigsRep armorConfigsRep;
        
        public override void InstallBindings()
        {
            Container.Bind<ArmorConfigsRep>().FromInstance(armorConfigsRep).AsSingle();
        }
    }
}
using App.Particles;
using UnityEngine;
using Zenject;

namespace App.Weapons.Shooting
{
    public class ShootFactoryInstaller : MonoInstaller
    {
        [SerializeField] private ParticleHolder bulletCollisionParticle;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShooterFactory>().FromNew().AsSingle().WithArguments(bulletCollisionParticle);
        }
    }
}
using App.Damage;
using App.Entities;
using App.Particles;

namespace App.Weapons.Shooting
{
    public class ShooterFactory
    {
        private readonly DamageApplicatorFactory _damageApplicatorFactory;
        private readonly ParticleFactory _particlesFactory;
        private readonly ParticleHolder _bulletCollisionParticlePrefab;

        public ShooterFactory(DamageApplicatorFactory damageApplicatorFactory, ParticleFactory particlesFactory, 
            ParticleHolder bulletCollisionParticlePrefab)
        {
            _damageApplicatorFactory = damageApplicatorFactory;
            _particlesFactory = particlesFactory;
            _bulletCollisionParticlePrefab = bulletCollisionParticlePrefab;
        }
        
        public Shooter CreateShoot(IEntity entity)
        {
            var damageApplicator = _damageApplicatorFactory.CreateDamageApplicator(entity.EntityType);
            return new Shooter(entity, damageApplicator, _particlesFactory, _bulletCollisionParticlePrefab);
        }
    }
}
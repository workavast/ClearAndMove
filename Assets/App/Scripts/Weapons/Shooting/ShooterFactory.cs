using App.Damage;
using App.Entities;
using App.Particles;

namespace App.Weapons.Shooting
{
    public class ShooterFactory
    {
        private readonly DamageApplicatorFactory _damageApplicatorFactory;
        private readonly ParticleFactory _particlesFactory;

        public ShooterFactory(DamageApplicatorFactory damageApplicatorFactory, ParticleFactory particlesFactory)
        {
            _damageApplicatorFactory = damageApplicatorFactory;
            _particlesFactory = particlesFactory;
        }
        
        public Shooter CreateShoot(IEntity entity)
        {
            var damageApplicator = _damageApplicatorFactory.CreateDamageApplicator(entity.EntityType);
            return new Shooter(entity, damageApplicator, _particlesFactory);
        }
    }
}
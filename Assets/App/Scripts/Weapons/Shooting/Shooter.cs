using App.Damage;
using App.Entities;
using App.Particles;
using Fusion;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace App.Weapons.Shooting
{
    public class Shooter
    {
        private Transform _shootPoint;
        private WeaponConfig _config;
        private readonly ParticleFactory _particlesFactory;
        private readonly ParticleHolder _bulletCollisionParticlePrefab;
        private readonly IDamageApplicator _damageApplicator;
        private readonly IEntity _entity;
        
        private NetworkRunner Runner => _entity.Runner;
        private PlayerRef InputAuthority => _entity.Object.InputAuthority;

        public Shooter(IEntity entity, IDamageApplicator damageApplicator, ParticleFactory particlesFactory, 
            ParticleHolder bulletCollisionParticlePrefab)
        {
            _entity = entity;
            _damageApplicator = damageApplicator;
            _particlesFactory = particlesFactory;
            _bulletCollisionParticlePrefab = bulletCollisionParticlePrefab;
        }

        public void SetData(Transform shootPoint, WeaponConfig config)
        {
            _shootPoint = shootPoint;
            _config = config;
        }
        
        public bool Shoot(bool hasStateAuthority, out ProjectileData projectileData, float spreadRatio, int hitLayers = -1)
        {
            var spreadAngle = _config.MinSpreadAngle + (_config.MaxSpreadAngle - _config.MinSpreadAngle) * spreadRatio;
            var spreadDirection = GetSpreadDirection(_shootPoint.forward, spreadAngle, Runner.Tick);
            
            const HitOptions hitOptions = HitOptions.IncludePhysX | HitOptions.IgnoreInputAuthority;
            var isHit = Runner.LagCompensation.Raycast(_shootPoint.position, spreadDirection, 100f, 
                InputAuthority, out var hit, hitLayers, hitOptions);
            
            if (isHit)
            {
                if (hit.GameObject == _entity.GameObject) 
                    Debug.LogWarning("Hit it self");

                if (hasStateAuthority)
                    _damageApplicator.TryApplyDamage(_config.DamagePerBullet, hit.GameObject, _entity);
                
                projectileData = new ProjectileData(hit.Point, hit.Normal);
                return true;
            }
            
            projectileData = default;
            return true;
        }

        public void ShootView(ref ProjectileData projectileData)
        {
            SpawnHitEffect(projectileData.HitPosition, projectileData.HitNormal);
        }
        
        public void OnDrawGizmos()
        {
            Gizmos.DrawLine(_shootPoint.position, _shootPoint.position + _shootPoint.forward * 100f);
        }
        
        private void SpawnHitEffect(Vector3 hitPoint, Vector3 normal) 
            => _particlesFactory.Create(_bulletCollisionParticlePrefab, hitPoint, normal);

        private static Vector3 GetSpreadDirection(Vector3 forward, float maxSpreadAngle, int tick)
        {
            var random = new Random((uint)tick);
            
            // for some reason first value from random isnt random
            // (with maxSpreadAngle = 10, spreadX slow decrease from 10, for each shoot (9.9 -> 9.8 -> 9.7 -> etc.))
            // so we call Next to avoid this un random random
            random.NextFloat(-maxSpreadAngle, maxSpreadAngle);

            var spreadX = random.NextFloat(-maxSpreadAngle, maxSpreadAngle);
            var spreadY = random.NextFloat(-maxSpreadAngle, maxSpreadAngle);
        
            var spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
            return spreadRotation * forward;
        }
    }
}

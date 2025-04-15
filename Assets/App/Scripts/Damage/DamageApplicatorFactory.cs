using System;
using App.Entities;

namespace App.Damage
{
    public class DamageApplicatorFactory
    {
        private readonly DamageApplicatorConfig _playersConfig;
        private readonly DamageApplicatorConfig _enemiesConfig;
        
        public DamageApplicatorFactory(DamageApplicatorConfig playersConfig, DamageApplicatorConfig enemiesConfig)
        {
            _playersConfig = playersConfig;
            _enemiesConfig = enemiesConfig;
        }

        public IDamageApplicator CreateDamageApplicator(EntityType owner)
        {
            return owner switch
            {
                EntityType.Default => CreateEnemyDamageApplicator(),
                EntityType.Player => CreatePlayerDamageApplicator(),
                _ => throw new ArgumentOutOfRangeException(nameof(owner), owner, null)
            };
        }

        private IDamageApplicator CreatePlayerDamageApplicator() 
            => new PlayerDamageApplicator(_playersConfig.HasFriendlyFire, _playersConfig.DamageScale);

        private IDamageApplicator CreateEnemyDamageApplicator() 
            => new EnemyDamageApplicator(_enemiesConfig.HasFriendlyFire, _enemiesConfig.DamageScale);
    }

    public struct DamageApplicatorConfig
    {
        public bool HasFriendlyFire;
        public float DamageScale;

        public DamageApplicatorConfig(bool hasFriendlyFire, float damageScale)
        {
            HasFriendlyFire = hasFriendlyFire;
            DamageScale = damageScale;
        }
    }
}
using System.Collections.Generic;
using App.Damage;

namespace App.App
{
    public class MissionInfrastructure
    {
        public DamageApplicatorConfig PlayerDamageConfig { get; private set; } = new(true, 1);
        public DamageApplicatorConfig EnemyDamageConfig { get; private set; } = new(true, 1);

        public IReadOnlyList<int> Levels => _levels;
        
        private readonly List<int> _levels = new(3); 

        public void SetLevels(IEnumerable<int> levels)
        {
            _levels.Clear();
            _levels.AddRange(levels);
        }
        
        public void SetPlayerFriendlyFire(bool hasFriendlyFire)
        {
            var damageConfig = PlayerDamageConfig;
            damageConfig.HasFriendlyFire = hasFriendlyFire;
            PlayerDamageConfig = damageConfig;
        }

        public void SetPlayerDamageScale(float damageScale) 
        {
            var damageConfig = PlayerDamageConfig;
            damageConfig.DamageScale = damageScale;
            PlayerDamageConfig = damageConfig;
        }

        public void SetEnemyFriendlyFire(bool hasFriendlyFire)
        {
            var damageConfig = EnemyDamageConfig;
            damageConfig.HasFriendlyFire = hasFriendlyFire;
            EnemyDamageConfig = damageConfig;
        }

        public void SetEnemyDamageScale(float damageScale) 
        {
            var damageConfig = EnemyDamageConfig;
            damageConfig.DamageScale = damageScale;
            EnemyDamageConfig = damageConfig;
        }
    }
}
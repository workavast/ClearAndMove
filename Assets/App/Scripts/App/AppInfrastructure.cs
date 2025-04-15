using App.Damage;

namespace App.App
{
    public static class AppInfrastructure
    {
        public static readonly MissionInfrastructure MissionInfrastructure = new();
    }
    
    public class MissionInfrastructure
    {
        public DamageApplicatorConfig PlayerDamageConfig { get; private set; } = new(true, 1);
        public DamageApplicatorConfig EnemyDamageConfig { get; private set; } = new(true, 1);

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
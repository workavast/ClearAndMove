using System;
using App.App;
using App.Entities;

namespace App.Damage
{
    public class DamageApplicatorFactory
    {
        private readonly IReadOnlyMissionModifiers _playersMissionModifiers;
        private readonly IReadOnlyMissionModifiers _enemiesMissionModifiers;
        
        public DamageApplicatorFactory(IReadOnlyMissionModifiers playersMissionModifiers, 
            IReadOnlyMissionModifiers enemiesMissionModifiers)
        {
            _playersMissionModifiers = playersMissionModifiers;
            _enemiesMissionModifiers = enemiesMissionModifiers;
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
            => new PlayerDamageApplicator(_playersMissionModifiers.HasFriendlyFire, _playersMissionModifiers.DamageScale);

        private IDamageApplicator CreateEnemyDamageApplicator() 
            => new EnemyDamageApplicator(_enemiesMissionModifiers.HasFriendlyFire, _enemiesMissionModifiers.DamageScale);
    }
}
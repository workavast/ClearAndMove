using App.Entities;

namespace App.Damage
{
    public class PlayerDamageApplicator : DamageApplicator
    {
        public PlayerDamageApplicator(bool hasFriendlyFire, float damageScale) 
            : base(hasFriendlyFire, damageScale) { }
        
        protected override void DamagePlayer(float damage, IDamageable receiver, IEntity shooter)
        {
            if (FriendlyFire)
                receiver.TakeDamage(damage * DamageScale, shooter);
        }

        protected override void DamageDefault(float damage, IDamageable receiver, IEntity shooter)
        {
            receiver.TakeDamage(damage * DamageScale, shooter);
        }
    }
}
using App.Armor;
using Avastrad.EventBusFramework;
using Fusion;
using Zenject;

namespace App.Entities.Enemy
{
    public class NetEnemy : NetEntity
    {
        public override EntityType EntityType => EntityType.Default;
        
        protected override IEventBus EventBus { get; set; }
        protected override ArmorConfigsRep ArmorConfigsRep { get; set; }

        private EnemiesRepository _enemiesRepository;

        [Inject]
        public void Construct(EnemiesRepository enemiesRepository, IEventBus eventBus, ArmorConfigsRep armorConfigsRep)
        {
            _enemiesRepository = enemiesRepository;
            EventBus = eventBus;
            ArmorConfigsRep = armorConfigsRep;
        }
        
        public override void Spawned()
        {
            base.Spawned();
            _enemiesRepository.Add(this);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            base.Despawned(runner, hasState);
            _enemiesRepository.Remove(this);
        }

        public override string GetName()
            => nameof(NetEnemy);
    }
}

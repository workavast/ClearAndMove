using App.Armor;
using App.EventBus;
using App.Players.Nicknames;
using App.WarFog;
using Avastrad.EventBusFramework;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Entities.Player
{
    public class NetPlayerEntity : NetEntity
    {
        [SerializeField] private FieldOfView fieldOfView;

        [Networked, SerializeField][field: ReadOnly] private bool IsScope { get; set; }
        
        public PlayerRef PlayerRef => Object.InputAuthority;
        public override EntityType EntityType => EntityType.Player;

        protected override IEventBus EventBus { get; set; }
        protected override ArmorConfigsRep ArmorConfigsRep { get; set; }

        private PlayersEntitiesRepository _playersEntitiesRepository;
        private NicknamesProvider _nicknamesProvider;
        
        [Inject]
        public void Construct(PlayersEntitiesRepository playersEntitiesRepository, NicknamesProvider nicknamesProvider, 
            IEventBus eventBus, ArmorConfigsRep armorConfigsRep)
        {
            _playersEntitiesRepository = playersEntitiesRepository;
            _nicknamesProvider = nicknamesProvider;
            EventBus = eventBus;
            ArmorConfigsRep = armorConfigsRep;
            
            OnKnockout += () => EventBus.Invoke(new OnPlayerKnockout());
            OnDeath += () => EventBus.Invoke(new OnPlayerDeath(PlayerRef));
            OnDeath += () => SetSelectState(true);
        }
        
        public override void Spawned()
        {
            base.Spawned();
            _playersEntitiesRepository.Add(Object.InputAuthority, this);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            base.Despawned(runner, hasState);
            _playersEntitiesRepository.Remove(this);
        }

        public override void Render()
        {
            base.Render();
            
            fieldOfView.SetScopeState(IsScope);
        }

        public override string GetName() 
            => _nicknamesProvider.GetNickName(PlayerRef);

        public void SetScopeState(bool isScope)
        {
            if ((HasStateAuthority || HasInputAuthority) && IsScope != isScope)
                IsScope = isScope;
        }

        public void SetSelectState(bool isSelectedEntity) 
            => fieldOfView.SetDynamicVisibilityState(isSelectedEntity && IsAlive);
    }
}


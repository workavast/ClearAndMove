using System;
using App.Armor;
using App.Dissolving;
using Fusion;
using UnityEngine;

namespace App.Entities
{
    public class EntityBody : NetworkBehaviour, IEntity
    {
        [SerializeField] private NetEntity netEntity;
        
        public bool IsSpawned => netEntity.IsSpawned;
        public EntityIdentifier Identifier => netEntity.Identifier;
        public EntityType EntityType => netEntity.EntityType;
        public GameObject GameObject => netEntity.GameObject;
        public new NetworkRunner Runner => netEntity.Runner;
        public new NetworkObject Object => netEntity.Object;
        public float NetHealthPoints => netEntity.NetHealthPoints;
        public int NetArmorLevel => netEntity.NetArmorLevel;
        public DissolvesUpdater DissolvesUpdater => netEntity.DissolvesUpdater;
        bool IEntity.IsAlive => netEntity.IsAlive;
        
        public event Action<IEntity> OnDeathEntity;

        public ArmorConfig GetArmor() => netEntity.GetArmor();
        
        public string GetName() => netEntity.GetName();

        public void TakeDamage(float damage, IEntity killer) => netEntity.TakeDamage(damage, killer);

        private void Awake()
        {
            netEntity.OnDeathEntity += (entity) => OnDeathEntity?.Invoke(entity);
        }
    }
}
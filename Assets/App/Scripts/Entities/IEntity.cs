using System;
using App.Armor;
using App.Damage;
using App.Dissolving;
using Fusion;
using UnityEngine;

namespace App.Entities
{
    public interface IEntity : IDamageable
    {
        bool IsSpawned { get; }
        EntityIdentifier Identifier { get; }
        EntityType EntityType { get; }
        GameObject GameObject { get; }
        Transform Transform => GameObject.transform;
        NetworkRunner Runner { get; }
        NetworkObject Object { get; }
        float NetHealthPoints { get; }
        int NetArmorLevel { get; }
        public DissolvesUpdater DissolvesUpdater { get; }
        bool IsAlive { get; }
        bool IsKnockout { get; }
        bool IsDead { get; }
        bool IsKnockoutOrDead { get; }

        public event Action<IEntity> OnDeathEntity;

        ArmorConfig GetArmor();
        string GetName();
    }

    public static class EntitiesExtension
    {
        public static bool Is(this IEntity source, IEntity other)
        {
            if (source == null && other == null)
                return true;
            if (source == null || other == null)
                return false;

            return source.Identifier.Id == other.Identifier.Id;
        }
        
        public static bool IsDeadOrKnockout(this IEntity source) => !source.IsAlive;
    }
}
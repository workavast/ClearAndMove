using System;
using Fusion;
using UnityEngine;

namespace App.Lobby.Missions.MissionModifiers
{
    public class NetMissionModifiersViewModel : NetworkBehaviour
    {
        [Networked] [OnChangedRender(nameof(DataChanged))] private bool NetPlayersFriendlyFire { get; set; }
        [Networked] [OnChangedRender(nameof(DataChanged))] private float NetPlayersDamageScale { get; set; }
        [Networked] [OnChangedRender(nameof(DataChanged))] private bool NetPlayerAutoReloading { get; set; }
        [Networked] [OnChangedRender(nameof(DataChanged))] private bool NetPlayerDropMagazine { get; set; }
        [Networked] [OnChangedRender(nameof(DataChanged))] private bool NetEnemiesFriendlyFire { get; set; }
        [Networked] [OnChangedRender(nameof(DataChanged))] private float NetEnemiesDamageScale { get; set; }
        
        [SerializeField] private MissionModifiersModel model;
        
        public bool PlayersFriendlyFire => model.PlayersFriendlyFire;
        public float PlayersDamageScale => model.PlayersDamageScale;
        public bool PlayersAutoReloading => model.PlayersAutoReloading;
        public bool PlayersDropMagazine => model.PlayersDropMagazine;
        
        public bool EnemiesFriendlyFire => model.EnemiesFriendlyFire;
        public float EnemiesDamageScale => model.EnemiesDamageScale;
        
        public event Action OnInitialized;
        public event Action OnDataChanged;

        public override void Spawned()
        {
            if (HasStateAuthority)
            {
                NetPlayersFriendlyFire = PlayersFriendlyFire;
                NetPlayersDamageScale = PlayersDamageScale;
                NetPlayerAutoReloading = PlayersAutoReloading;
                NetPlayerDropMagazine = PlayersDropMagazine;
                
                NetEnemiesFriendlyFire = EnemiesFriendlyFire;
                NetEnemiesDamageScale = EnemiesDamageScale;
            }

            OnInitialized?.Invoke();
            OnDataChanged?.Invoke();
        }

        public void SetPlayerFriendlyFire(bool hasFriendlyFire)
        {
            if (!HasStateAuthority)
            {
                Debug.LogWarning("You try modifiers when you are not host");
                return;
            }
            
            NetPlayersFriendlyFire = hasFriendlyFire;
        }

        public void SetPlayerDamageScale(float damageScale)
        {
            if (!HasStateAuthority)
            {
                Debug.LogWarning("You try modifiers when you are not host");
                return;
            }
            
            NetPlayersDamageScale = damageScale;
        }
        
        public void SetPlayerAutoReloading(bool autoReloading)
        {
            if (!HasStateAuthority)
            {
                Debug.LogWarning("You try modifiers when you are not host");
                return;
            }
            
            NetPlayerAutoReloading = autoReloading;
        }
        
        public void SetPlayerDropMagazine(bool dropMagazine)
        {
            if (!HasStateAuthority)
            {
                Debug.LogWarning("You try modifiers when you are not host");
                return;
            }
            
            NetPlayerDropMagazine = dropMagazine;
        }

        public void SetEnemyFriendlyFire(bool hasFriendlyFire)
        {
            if (!HasStateAuthority)
            {
                Debug.LogWarning("You try modifiers when you are not host");
                return;
            }
            
            NetEnemiesFriendlyFire = hasFriendlyFire;
        }

        public void SetEnemyDamageScale(float damageScale)
        {
            if (!HasStateAuthority)
            {
                Debug.LogWarning("You try modifiers when you are not host");
                return;
            }
            
            NetEnemiesDamageScale = damageScale;
        }

        public void DataChanged()
        {
            model.SetPlayerFriendlyFire(NetPlayersFriendlyFire);
            model.SetPlayerDamageScale(NetPlayersDamageScale);
            model.SetPlayerAutoReloading(NetPlayerAutoReloading);
            model.SetPlayerDropMagazine(NetPlayerDropMagazine);
            
            model.SetEnemyFriendlyFire(NetEnemiesFriendlyFire);
            model.SetEnemyDamageScale(NetEnemiesDamageScale);
            
            OnDataChanged?.Invoke();
        }
    }
}
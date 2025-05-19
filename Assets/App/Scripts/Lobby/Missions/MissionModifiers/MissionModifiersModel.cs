using App.App;
using UnityEngine;

namespace App.Lobby.Missions.MissionModifiers
{
    public class MissionModifiersModel : MonoBehaviour
    {
        public bool PlayersFriendlyFire => AppInfrastructure.PlayerMissionModifiers.HasFriendlyFire;
        public float PlayersDamageScale => AppInfrastructure.PlayerMissionModifiers.DamageScale;
        public bool PlayersAutoReloading => AppInfrastructure.PlayerMissionModifiers.AutoReloading;
        public bool PlayersDropMagazine => AppInfrastructure.PlayerMissionModifiers.DropMagazineOnReloading;
        public bool EnemiesFriendlyFire => AppInfrastructure.EnemyMissionModifiers.HasFriendlyFire;
        public float EnemiesDamageScale => AppInfrastructure.EnemyMissionModifiers.DamageScale;
        
        public void SetPlayerFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.PlayerMissionModifiers.HasFriendlyFire = hasFriendlyFire;

        public void SetPlayerDamageScale(float damageScale) 
            => AppInfrastructure.PlayerMissionModifiers.DamageScale = damageScale;
        
        public void SetPlayerAutoReloading(bool autoReloading) 
            => AppInfrastructure.PlayerMissionModifiers.AutoReloading = autoReloading;
        
        public void SetPlayerDropMagazine(bool dropMagazine) 
            => AppInfrastructure.PlayerMissionModifiers.DropMagazineOnReloading = dropMagazine;
        
        public void SetEnemyFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.EnemyMissionModifiers.HasFriendlyFire = hasFriendlyFire;
        
        public void SetEnemyDamageScale(float damageScale) 
            => AppInfrastructure.EnemyMissionModifiers.DamageScale = damageScale;
    }
}
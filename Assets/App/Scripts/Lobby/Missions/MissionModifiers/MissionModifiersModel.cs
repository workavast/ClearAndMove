using App.App;
using UnityEngine;

namespace App.Lobby.Missions.MissionModifiers
{
    public class MissionModifiersModel : MonoBehaviour
    {
        public bool PlayersFriendlyFire => AppInfrastructure.MissionInfrastructure.PlayerDamageConfig.HasFriendlyFire;
        public float PlayersDamageScale => AppInfrastructure.MissionInfrastructure.PlayerDamageConfig.DamageScale;
        public bool EnemiesFriendlyFire => AppInfrastructure.MissionInfrastructure.EnemyDamageConfig.HasFriendlyFire;
        public float EnemiesDamageScale => AppInfrastructure.MissionInfrastructure.EnemyDamageConfig.DamageScale;
        
        public void SetPlayerFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.MissionInfrastructure.SetPlayerFriendlyFire(hasFriendlyFire);

        public void SetPlayerDamageScale(float damageScale) 
            => AppInfrastructure.MissionInfrastructure.SetPlayerDamageScale(damageScale);
        
        public void SetEnemyFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.MissionInfrastructure.SetEnemyFriendlyFire(hasFriendlyFire);
        
        public void SetEnemyDamageScale(float damageScale) 
            => AppInfrastructure.MissionInfrastructure.SetEnemyDamageScale(damageScale);
    }
}
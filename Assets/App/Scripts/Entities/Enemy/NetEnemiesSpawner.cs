using App.Weapons;
using Fusion;
using UnityEngine;

namespace App.Entities.Enemy
{
    public class NetEnemiesSpawner : NetworkBehaviour
    {
        [SerializeField] private NetEnemy netEnemyPrefab;
        
        public override void Spawned()
        {
            if (!HasStateAuthority)
                return;

            var spawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
            foreach (var spawnPoint in spawnPoints)
            {
                var enemy = Runner.Spawn(netEnemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.SetWeapon(WeaponId.ScarEnemy);
            }
        }
    }
}
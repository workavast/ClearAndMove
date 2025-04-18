using App.NetworkRunning;
using App.Weapons;
using UnityEngine;
using Zenject;

namespace App.Entities.Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private NetEnemy netEnemyPrefab;

        [Inject] private readonly NetworkRunnerProvider _runnerProvider;

        public NetEnemy Create(Transform spawnPoint, int armorLevel, WeaponId initialWeapon = WeaponId.Pistol)
            => Create(spawnPoint.position, spawnPoint.rotation, armorLevel, initialWeapon);
        
        public NetEnemy Create(Vector3 position, int armorLevel, WeaponId initialWeapon = WeaponId.Pistol)
            => Create(position, Quaternion.identity, armorLevel, initialWeapon);

        public NetEnemy Create(Vector3 position, Quaternion rotation, int armorLevel, WeaponId initialWeapon = WeaponId.Pistol)
        {
            var runner = _runnerProvider.GetNetworkRunner();
            var netPlayerController = runner.Spawn(netEnemyPrefab, position, rotation);
            netPlayerController.SetWeapon(initialWeapon);
            netPlayerController.SetArmor(armorLevel);
            return netPlayerController;
        }
    }
}
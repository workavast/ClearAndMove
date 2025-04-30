using App.Dissolving;
using App.Entities.Enemy;
using App.Players;
using UnityEngine;
using Zenject;

namespace App.Missions.Levels
{
    public class LevelVisibilityToggler : MonoBehaviour
    {
        [SerializeField] private DissolvesUpdater dissolvesUpdater;
        [SerializeField] private BoxCollider levelAndAboveCollider;
        [SerializeField] private BoxCollider levelCollider;

        [Inject] private readonly EnemiesRepository _enemiesRepository;
        [Inject] private readonly LocalPlayerProvider _localPlayerProvider;

        private void Start()
        {
            dissolvesUpdater.SetValue(1);

            foreach (var enemy in _enemiesRepository.Enemies) 
                InitEnemyDissolving(enemy);

            _enemiesRepository.OnAdd += InitEnemyDissolving;
        }

        private void OnDestroy()
        {
            _enemiesRepository.OnAdd -= InitEnemyDissolving;
        }

        private void LateUpdate()
        {
            if (_localPlayerProvider.HasEntity)
            {
                var playerOnTheLevelOrAbove = InZone(levelAndAboveCollider, _localPlayerProvider.Position);
                if (playerOnTheLevelOrAbove != dissolvesUpdater.IsVisible)
                {
                    foreach (var enemy in _enemiesRepository.Enemies)
                    {
                        var enemyOnTheLevel = InZone(levelCollider, enemy.transform.position);
                        if (enemyOnTheLevel) 
                            enemy.DissolvesUpdater.SetVisibilityState(playerOnTheLevelOrAbove);
                    }
                    
                    dissolvesUpdater.SetVisibilityState(playerOnTheLevelOrAbove);
                }
            }
        }

        private void InitEnemyDissolving(NetEnemy enemy)
        {
            if (_localPlayerProvider.HasEntity)
            {
                var enemyOnTheLevel = InZone(levelCollider, enemy.transform.position);
                if (enemyOnTheLevel)
                {
                    var playerOnTheLevelOrAbove = InZone(levelAndAboveCollider, _localPlayerProvider.Position);
                    if (playerOnTheLevelOrAbove)
                    {
                        dissolvesUpdater.SetValue(1);
                        enemy.DissolvesUpdater.SetVisibilityState(true);                
                    }
                    else
                    {
                        dissolvesUpdater.SetValue(0);
                        enemy.DissolvesUpdater.SetVisibilityState(false);
                    }
                }
            }
        }
        
        private static bool InZone(Collider checkCollider, Vector3 position) 
            => checkCollider.bounds.Contains(position);
    }
}
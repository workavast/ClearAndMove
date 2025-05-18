using App.Dissolving;
using App.Entities.Enemy;
using App.Entities.Player.SelectionPlayerEntity;
using Avastrad.Extensions;
using UnityEngine;
using Zenject;

namespace App.Missions.Levels
{
    public class LevelVisibilityToggler : MonoBehaviour
    {
        [SerializeField] private DissolvesUpdater internalDissolvesUpdater;
        [SerializeField] private DissolvesUpdater externalDissolvesUpdater;
        [SerializeField] private BoxCollider levelAndAboveCollider;
        [SerializeField] private BoxCollider levelCollider;

        [Inject] private readonly EnemiesRepository _enemiesRepository;
        [Inject] private readonly SelectedPlayerEntityProvider _playerProvider;

        private bool _playerOnTheLevel;
        private bool _playerOnTheLevelOrAbove;
        
        private void Start()
        {
            if (_playerProvider.HasEntity)
            {
                _playerOnTheLevel = InZone(levelCollider, _playerProvider.Position);
                _playerOnTheLevelOrAbove = _playerOnTheLevel || InZone(levelAndAboveCollider, _playerProvider.Position);
            }

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
            if (_playerProvider.HasEntity)
            {
                var prevPlayerOnTheLevelOrAbove = _playerOnTheLevelOrAbove;
                
                var playerOnTheLevel = InZone(levelCollider, _playerProvider.Position);
                var playerOnTheLevelOrAbove = playerOnTheLevel || InZone(levelAndAboveCollider, _playerProvider.Position);
                
                if (_playerOnTheLevelOrAbove != playerOnTheLevelOrAbove)
                {
                    _playerOnTheLevelOrAbove = playerOnTheLevelOrAbove;
                    foreach (var enemy in _enemiesRepository.Enemies)
                    {
                        var enemyOnTheLevel = InZone(levelCollider, enemy.transform.position);
                        if (enemyOnTheLevel) 
                            enemy.DissolvesUpdater.SetVisibilityState(_playerOnTheLevelOrAbove, false);
                    }
                    
                    externalDissolvesUpdater.SetVisibilityState(_playerOnTheLevelOrAbove, false);
                }

                if (_playerOnTheLevel != playerOnTheLevel)
                {
                    var moveToTheNExtLevelOrDownToTheThisLevel = _playerOnTheLevelOrAbove == prevPlayerOnTheLevelOrAbove;
                    _playerOnTheLevel = playerOnTheLevel;
                    internalDissolvesUpdater.SetVisibilityState(_playerOnTheLevel, moveToTheNExtLevelOrDownToTheThisLevel);
                }
            }
        }

        private void InitEnemyDissolving(NetEnemy enemy)
        {
            var enemyOnTheLevel = InZone(levelCollider, enemy.transform.position);
            if (enemyOnTheLevel) 
                enemy.DissolvesUpdater.SetVisibilityState(_playerOnTheLevelOrAbove, false);
        }
        
        private static bool InZone(Collider checkCollider, Vector3 position) 
            => checkCollider.ContainsByY(position);
    }
}
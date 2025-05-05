using System.Collections.Generic;
using App.Dissolving;
using App.Entities.Enemy;
using App.Entities.Player.SelectionPlayerEntity;
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
        [SerializeField] private bool isStaticDissolve;

        [Inject] private readonly EnemiesRepository _enemiesRepository;
        [Inject] private readonly SelectedPlayerEntityProvider _playerProvider;

        private readonly List<RenderersHolder> _renderersHolders = new();

        private bool _playerOnTheLevel = false;
        private bool _playerOnTheLevelOrAbove = false;
        
        private void Awake()
        {
            _renderersHolders.AddRange(GetComponentsInChildren<RenderersHolder>(true));
        }

        private void Start()
        {
            if (_playerProvider.HasEntity)
            {
                _playerOnTheLevel = InZone(levelCollider, _playerProvider.Position);
                _playerOnTheLevelOrAbove = InZone(levelAndAboveCollider, _playerProvider.Position);
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
                var playerOnTheLevel = InZone(levelCollider, _playerProvider.Position);
                var playerOnTheLevelOrAbove = InZone(levelAndAboveCollider, _playerProvider.Position);
                
                if (_playerOnTheLevelOrAbove != playerOnTheLevelOrAbove)
                {
                    _playerOnTheLevelOrAbove = playerOnTheLevelOrAbove;
                    foreach (var enemy in _enemiesRepository.Enemies)
                    {
                        var enemyOnTheLevel = InZone(levelCollider, enemy.transform.position);
                        if (enemyOnTheLevel) 
                            TrySetDissolve(enemy.DissolvesUpdater, _playerOnTheLevelOrAbove);
                    }
                    
                    TrySetDissolve(externalDissolvesUpdater, _playerOnTheLevelOrAbove);
                }

                if (_playerOnTheLevel != playerOnTheLevel)
                {
                    _playerOnTheLevel = playerOnTheLevel;
                    TrySetDissolve(internalDissolvesUpdater, _playerOnTheLevel);
                    foreach (var renderersHolder in _renderersHolders)
                        renderersHolder.SetRenderState(_playerOnTheLevel);
                }
            }
        }

        private void TrySetDissolve(DissolvesUpdater dissolvesUpdater, bool isVisible)
        {
            if (isStaticDissolve)
                return;
            
            dissolvesUpdater.SetVisibilityState(isVisible);
        }
        
        private void InitEnemyDissolving(NetEnemy enemy)
        {
            var enemyOnTheLevel = InZone(levelCollider, enemy.transform.position);
            if (enemyOnTheLevel) 
                TrySetDissolve(enemy.DissolvesUpdater, _playerOnTheLevelOrAbove);
        }
        
        private static bool InZone(Collider checkCollider, Vector3 position) 
            => checkCollider.ContainsByY(position);
    }
}
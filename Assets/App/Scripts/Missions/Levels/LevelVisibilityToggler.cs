using System;
using App.Dissolving;
using App.Players;
using UnityEngine;
using Zenject;

namespace App.Missions
{
    public class LevelVisibilityToggler : MonoBehaviour
    {
        [SerializeField] private DissolvesUpdater dissolvesUpdater;
        [SerializeField] private BoxCollider boxCollider;

        [Inject] private readonly LocalPlayerProvider _localPlayerProvider;
        
        private void LateUpdate()
        {
            if (_localPlayerProvider.HasEntity)
            {
                var onTheLevel = boxCollider.bounds.Contains(_localPlayerProvider.Position);
                dissolvesUpdater.SetVisibilityState(onTheLevel);
            }
        }
    }
}
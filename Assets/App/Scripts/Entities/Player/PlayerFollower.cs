using App.Players;
using UnityEngine;
using Zenject;

namespace App.Entities.Player
{
    public class PlayerFollower : MonoBehaviour
    {
        [Inject] private readonly LocalPlayerProvider _localPlayerProvider;

        private void Update()
        {
            if (_localPlayerProvider.HasEntity) 
                transform.position = _localPlayerProvider.Position;
        }
    }
}
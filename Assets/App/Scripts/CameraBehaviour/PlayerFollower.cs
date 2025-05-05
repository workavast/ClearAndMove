using App.Entities.Player.SelectionPlayerEntity;
using UnityEngine;
using Zenject;

namespace App.CameraBehaviour
{
    public class PlayerFollower : MonoBehaviour
    {
        [Inject] private readonly SelectedPlayerEntityProvider _selectedPlayerEntityProvider;

        private void Update()
        {
            if (_selectedPlayerEntityProvider.HasEntity) 
                transform.position = _selectedPlayerEntityProvider.Position;
        }
    }
}
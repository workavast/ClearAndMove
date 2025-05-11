using App.Entities.Player.SelectionPlayerEntity;
using TMPro;
using UnityEngine;
using Zenject;

namespace App.UI.PlayerEntityDataView
{
    public class PlayerEntityFullAmmoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text fullAmmoView;
            
        [Inject] private readonly SelectedPlayerEntityProvider _playerProvider;

        private void LateUpdate()
        {
            if (_playerProvider.HasEntity)
                fullAmmoView.text = $"{_playerProvider.FullAmmoSize}";
            else
                fullAmmoView.text = $"";
        }
    }
}
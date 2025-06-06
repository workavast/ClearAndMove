using App.Entities.Player.SelectionPlayerEntity;
using TMPro;
using UnityEngine;
using Zenject;

namespace App.UI.PlayerEntityDataView
{
    public class PlayerEntityAmmoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text magazineView;
            
        [Inject] private readonly SelectedPlayerEntityProvider _playerProvider;

        private void LateUpdate()
        {

            if (_playerProvider.HasEntity)
                magazineView.text = $"{_playerProvider.CurrentMagazineAmmo:#0}/{_playerProvider.MaxMagazineAmmo:#0}";
            else
                magazineView.text = $"";
        }
    }
}
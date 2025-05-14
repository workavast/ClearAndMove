using App.Entities.Player.SelectionPlayerEntity;
using Avastrad.UI.Elements.BarView;
using TMPro;
using UnityEngine;
using Zenject;

namespace App.UI.PlayerEntityDataView
{
    public class PlayerEntityHealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textView;
        [SerializeField] private Bar barView;
            
        [Inject] private readonly SelectedPlayerEntityProvider _playerProvider;

        private void LateUpdate()
        {
            if (_playerProvider.HasEntity)
            {
                barView.SetValue(_playerProvider.CurrentHealthPoints / _playerProvider.MaxHealthPoints);
                var currentHealthRounded = Mathf.CeilToInt(_playerProvider.CurrentHealthPoints);
                textView.text = $"{currentHealthRounded:#0}/{_playerProvider.MaxHealthPoints:#0}";
            }
            else
            {
                barView.SetValue(0);
                textView.text = $"";
            }
        }
    }
}
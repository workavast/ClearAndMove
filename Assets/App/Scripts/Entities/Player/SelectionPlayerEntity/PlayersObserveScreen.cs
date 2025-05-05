using App.PlayerInput.InputProviding;
using Avastrad.UI.UiSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Entities.Player.SelectionPlayerEntity
{
    public class PlayersObserveScreen : ScreenBase
    {
        [SerializeField] private Button nextPlayerBtn;
        [SerializeField] private Button prevPlayerBtn;

        [Inject] private readonly SelectedPlayerEntityChanger _selectedPlayerEntityChanger;
        [Inject] private readonly IInputProvider _inputProvider;

        private void Awake()
        {
            nextPlayerBtn.onClick.AddListener(_selectedPlayerEntityChanger.NextTarget);
            prevPlayerBtn.onClick.AddListener(_selectedPlayerEntityChanger.PrevTarget);
        }

        private void Update()
        {
            if (_inputProvider.NextPlayer)
            {
                _selectedPlayerEntityChanger.NextTarget();
                return;
            }

            if (_inputProvider.PrevPlayer)
            {
                _selectedPlayerEntityChanger.PrevTarget();
                return;
            }
        }

        private void OnDestroy()
        {
            nextPlayerBtn.onClick.RemoveListener(_selectedPlayerEntityChanger.NextTarget);
            prevPlayerBtn.onClick.RemoveListener(_selectedPlayerEntityChanger.PrevTarget);
        }
    }
}
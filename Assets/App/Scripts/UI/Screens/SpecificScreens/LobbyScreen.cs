using App.CursorBehaviour;
using Avastrad.UI.UiSystem;
using Zenject;

namespace App.UI.Screens
{
    public class LobbyScreen : ScreenBase
    {
        [Inject] private readonly CursorVisibilityBehaviour _cursorVisibilityBehaviour;

        private void OnEnable()
        {
            _cursorVisibilityBehaviour.Show();
        }

        private void OnDisable()
        {
            _cursorVisibilityBehaviour.Hide();
        }
    }
}
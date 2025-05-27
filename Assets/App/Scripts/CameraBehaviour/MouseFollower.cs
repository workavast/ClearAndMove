using App.Entities.Player.SelectionPlayerEntity;
using App.PlayerInput.InputProviding;
using Avastrad.Extensions;
using UnityEngine;
using Zenject;

namespace App.CameraBehaviour
{
    public class MouseFollower : MonoBehaviour
    {
        [Inject] private readonly SelectedPlayerEntityProvider _localPlayerProvider;
        [Inject] private readonly RawInputProvider _rawInputProvider;

        private Vector3 _lastPlayerPosition;
        
        private void Update()
        {
            if (_rawInputProvider.MouseOverUI())
                return;

            var depthOffset = Camera.main.transform.position.y;
            var screenPoint = _rawInputProvider.MousePosition.XY0(depthOffset);
            var lookPoint = Camera.main.ScreenToWorldPoint(screenPoint);

            if (_localPlayerProvider.HasEntity)
                _lastPlayerPosition = _localPlayerProvider.Position;

            lookPoint.y = _lastPlayerPosition.y;
            transform.position = lookPoint;
        }
    }
}
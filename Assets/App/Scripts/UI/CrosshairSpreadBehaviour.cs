using App.Entities.Player;
using App.PlayerInput.InputProviding;
using Avastrad.Extensions;
using UnityEngine;
using Zenject;

namespace App
{
    public class CrosshairSpreadBehaviour : MonoBehaviour
    {
        [SerializeField] private RectTransform crosshair;
        [SerializeField] private float scale = 1;
        
        [Inject] private readonly LocalPlayerProvider _localPlayerProvider;
        [Inject] private readonly RawInputProvider _rawInputProvider;

        private void LateUpdate()
        {
            if (!_localPlayerProvider.HasEntity || !_localPlayerProvider.IsAlive)
            {
                crosshair.sizeDelta = new Vector2(0, 0);
                return;
            }

            var depthOffset = Camera.main.transform.position.y;
            var screenPoint = _rawInputProvider.MousePosition.XY0(depthOffset);
            var lookPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            lookPoint.y = _localPlayerProvider.Position.y;
            
            var spreadAngle = _localPlayerProvider.SpreadAngle;
            var distance = Vector3.Distance(_localPlayerProvider.Position, lookPoint);
            crosshair.sizeDelta = new Vector2(spreadAngle * distance * scale, spreadAngle * distance * scale);
        }
    }
}
using UnityEngine;

namespace App.Dissolving
{
    public class DissolvesUpdater : MonoBehaviour
    {
        [SerializeField] private DissolvesOwner dissolvesOwner;
        [SerializeField] private DissolveConfig config;

        private float Duration => config.Duration;
        private bool _dissolve;
        private float _timer;
        
        private void LateUpdate()
        {
            if (_dissolve && _timer > Duration)
                return;

            if (!_dissolve && _timer < 0)
                return;

            if (_dissolve)
                _timer += Time.deltaTime;
            else
                _timer -= Time.deltaTime;

            dissolvesOwner.ManualUpdate(_timer / Duration);
        }

        public void SetVisibilityState(bool isVisible)
        {
            _dissolve = !isVisible;
        }

        public void SetValue(float visibilityValue)
        {
            _timer = visibilityValue * Duration;
        }
    }
}
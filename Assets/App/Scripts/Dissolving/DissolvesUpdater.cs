using System;
using UnityEngine;

namespace App.Dissolving
{
    public class DissolvesUpdater : MonoBehaviour
    {
        [SerializeField] private DissolvesOwner dissolvesOwner;
        [SerializeField] private DissolveConfig config;
        [field: SerializeField] public bool IsVisible { get; private set; } = true;
        
        private float Duration => config.Duration;
        private float _dissolveTimer;

        private void Start()
        {
            if (IsVisible)
                SetValue(Duration);
            else
                SetValue(0);
        }

        private void LateUpdate()
        {
            if (!IsVisible && _dissolveTimer > Duration)
                return;

            if (IsVisible && _dissolveTimer < 0)
                return;

            if (IsVisible)
                _dissolveTimer -= Time.deltaTime;
            else
                _dissolveTimer += Time.deltaTime;

            dissolvesOwner.ManualUpdate(_dissolveTimer / Duration);
        }

        public void SetVisibilityState(bool isVisible)
        {
            IsVisible = isVisible;
        }

        /// <param name="visibilityValue">[0,1] where 0 is invisible</param>
        public void SetValue(float visibilityValue)
        {
            _dissolveTimer = visibilityValue * Duration;
            dissolvesOwner.ManualUpdate(_dissolveTimer / Duration);
        }
    }
}
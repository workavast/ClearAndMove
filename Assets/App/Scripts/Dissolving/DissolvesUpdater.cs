using System.Collections.Generic;
using UnityEngine;

namespace App.Dissolving
{
    public class DissolvesUpdater : MonoBehaviour
    {
        [SerializeField] private DissolveConfig config;
        [field: SerializeField] public bool IsVisible { get; private set; } = true;

        private readonly List<DissolveOwner> _dissolveOwners = new();
        
        private float Duration => config.Duration;
        private float _dissolveTimer;

        private void Awake()
        {
            var dissolveOwners = GetComponentsInChildren<DissolveOwner>(true);
            _dissolveOwners.AddRange(dissolveOwners);
        }

        private void Start()
        {
            if (IsVisible)
                SetValue(1);
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

            UpdateDissolves(_dissolveTimer / Duration);
        }

        public void SetVisibilityState(bool isVisible)
        {
            IsVisible = isVisible;
        }

        /// <param name="visibilityValue">[0,1] where 0 is invisible</param>
        public void SetValue(float visibilityValue)
        {
            _dissolveTimer = visibilityValue * Duration;
            UpdateDissolves(_dissolveTimer / Duration);
        }

        public void AddDissolveOwner(DissolveOwner dissolveOwner)
        {
            _dissolveOwners.Add(dissolveOwner);
            dissolveOwner.ManualUpdate(_dissolveTimer / Duration);
        }

        public void RemoveDissolveOwner(DissolveOwner dissolveOwner) 
            => _dissolveOwners.Remove(dissolveOwner);

        private void UpdateDissolves(float percentageValue)
        {
            foreach (var dissolveOwner in _dissolveOwners) 
                dissolveOwner.ManualUpdate(percentageValue);
        }
    }
}
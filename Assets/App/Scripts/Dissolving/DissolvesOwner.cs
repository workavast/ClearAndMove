using System;
using UnityEngine;

namespace App.Dissolving
{
    public class DissolvesOwner : MonoBehaviour
    {
        private DissolveOwner[] _dissolveOwners = Array.Empty<DissolveOwner>();

        private void Awake()
        {
            _dissolveOwners = GetComponentsInChildren<DissolveOwner>(true);
        }
        
        public void ManualUpdate(float percentageValue)
        {
            foreach (var material in _dissolveOwners) 
                material.ManualUpdate(percentageValue);
        }
    }
}
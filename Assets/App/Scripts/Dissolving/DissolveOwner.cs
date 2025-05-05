using System.Collections.Generic;
using UnityEngine;

namespace App.Dissolving
{
    public class DissolveOwner : MonoBehaviour
    {
        private readonly List<Renderer> _renderers = new();
        private readonly List<Material> _materials = new();
        private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");

        private bool _isRender;
        
        private void Awake()
        {
            var renders = GetComponentsInChildren<Renderer>(true);
            _renderers.Capacity = renders.Length;
            _materials.Capacity = renders.Length;
            
            foreach (var someRenderer in renders)
            {
                _renderers.Add(someRenderer);
                _materials.AddRange(someRenderer.materials);
            }
        }

        private void Start()
        {
            var dissolvesUpdater = GetComponentInParent<DissolvesUpdater>();
            if (dissolvesUpdater != null) 
                dissolvesUpdater.AddDissolveOwner(this);
        }

        private void OnDestroy()
        {
            var dissolvesUpdater = GetComponentInParent<DissolvesUpdater>();
            if (dissolvesUpdater != null) 
                dissolvesUpdater.RemoveDissolveOwner(this);
        }

        public void ManualUpdate(float percentageValue)
        {
            if (!_isRender && percentageValue >= 1)
                return;

            if (percentageValue >= 1 && _isRender)
            {
                _isRender = false;
                SetRenderState(false);
            }

            if (percentageValue < 1 && !_isRender)
            {
                _isRender = true;
                SetRenderState(true);
            }
            
            foreach (var material in _materials) 
                material.SetFloat(Dissolve, percentageValue);
        }

        public void SetRenderState(bool isRender)
        {
            foreach (var someRenderer in _renderers) 
                someRenderer.enabled = isRender;
        }
    }
}
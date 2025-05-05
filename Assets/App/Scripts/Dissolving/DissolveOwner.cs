using System.Collections.Generic;
using UnityEngine;

namespace App.Dissolving
{
    [RequireComponent(typeof(RenderersHolder))]
    public class DissolveOwner : MonoBehaviour
    {
        private bool _isRender = true;
        private RenderersHolder _renderersHolder;
        private readonly List<Material> _materials = new();
        private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");
        
        private void Awake()
        {
            _renderersHolder = GetComponent<RenderersHolder>();
        }

        private void Start()
        {
            var renders = _renderersHolder.Renderers;
            _materials.Capacity = renders.Count;
            foreach (var someRenderer in renders) 
                _materials.AddRange(someRenderer.materials);
            
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
            => _renderersHolder.SetRenderState(isRender);
    }
}
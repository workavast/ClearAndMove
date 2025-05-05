using System.Collections.Generic;
using UnityEngine;

namespace App.Dissolving
{
    public class RenderersHolder : MonoBehaviour
    {
        public bool IsRender => _renderRequestsCount > 0;
        public IReadOnlyList<Renderer> Renderers => _renderers;
        
        private readonly List<Renderer> _renderers = new();
        private int _renderRequestsCount;

        private void Awake()
        {
            _renderRequestsCount = 1;
            var renders = GetComponentsInChildren<Renderer>(true);
            _renderers.Capacity = renders.Length;

            foreach (var someRenderer in renders)
                _renderers.Add(someRenderer);
        }

        public void SetRenderState(bool isRender)
        {
            var prevRenderRequestsCount = _renderRequestsCount;
            if (isRender)
                _renderRequestsCount++;
            else
                _renderRequestsCount--;

            if (prevRenderRequestsCount == 0 && _renderRequestsCount == 1)
                SetRenderEnabled(true);
            else if (prevRenderRequestsCount == 1 && _renderRequestsCount == 0) 
                SetRenderEnabled(false);
        }

        private void SetRenderEnabled(bool isEnabled)
        {
            foreach (var someRenderer in _renderers)
                someRenderer.enabled = isEnabled;
        }
    }
}
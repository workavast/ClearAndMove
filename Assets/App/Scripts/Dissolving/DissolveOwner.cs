using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Dissolving
{
    public class DissolveOwner : MonoBehaviour
    {
        private List<Material> _materials;
        private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");

        private void Awake()
        {
            var renders = GetComponentsInChildren<Renderer>(true);
            _materials = new List<Material>(renders.Length);
            
            foreach (var renderer in renders)
                _materials.AddRange(renderer.materials);
        }

        public void ManualUpdate(float percentageValue)
        {
            foreach (var material in _materials) 
                material.SetFloat(Dissolve, percentageValue);
        }
    }
}
using System;
using System.Collections.Generic;
using Avastrad.PoolSystem;
using UnityEngine;
using Zenject;

namespace App.Particles
{
    public class ParticleFactory
    {
        private readonly Transform _mainParent;
        private readonly DiContainer _diContainer;
        private readonly Dictionary<string, Transform> _parents = new();
        private readonly Dictionary<string, Pool<ParticleHolder>> _pools = new();
        
        public ParticleFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _mainParent = new GameObject { transform = { name = "Particles" } }.transform;
        }

        public ParticleHolder Create(ParticleHolder prefab, Vector3 position, Vector3 normal) 
            => Create(prefab, position, Quaternion.LookRotation(normal));

        public ParticleHolder Create(ParticleHolder prefab, Transform transform) 
            => Create(prefab, transform.position, transform.rotation);
        
        public ParticleHolder Create(ParticleHolder prefab, Vector3 position, Quaternion rotation) 
        {
            var key = prefab.gameObject.name;
            return Create(key, prefab, position, rotation);
        }

        private ParticleHolder Create(string key, ParticleHolder prefab, Vector3 position, Quaternion rotation)
        {
            if (!_pools.ContainsKey(key))
            {
                _pools.Add(key, new Pool<ParticleHolder>(null));

                var parent = new GameObject { transform = { name = key , parent = _mainParent} }.transform;
                _parents.Add(key, parent);
            }
            
            Func<ParticleHolder> delegat = () => InstantiateEntity(prefab, key);
            _pools[key].ExtractElement(out var element, delegat);
            element.transform.SetPositionAndRotation(position, rotation);

            return element;
        }

        private ParticleHolder InstantiateEntity(ParticleHolder prefab, string key)
        {
            var instance = _diContainer.InstantiatePrefab(prefab, _parents[key]).GetComponent<ParticleHolder>();
            return instance;
        }
    }
}
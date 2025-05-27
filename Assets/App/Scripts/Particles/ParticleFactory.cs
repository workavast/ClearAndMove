using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using Avastrad.EnumValuesLibrary;
using Avastrad.PoolSystem;
using UnityEngine;

namespace App.Particles
{
    public class ParticleFactory : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<ParticleType, ParticleHolder> particleHolderPrefabs;

        private Pool<ParticleHolder, ParticleType> _pool;
        
        private readonly Dictionary<ParticleType, Transform> _parents = new();
        
        private void Awake()
        {
            _pool = new Pool<ParticleHolder, ParticleType>(Instantiate);
            
            var particleTypes = EnumValuesTool.GetValues<ParticleType>();
            foreach (var particleType in particleTypes)
            {
                var parent = new GameObject()
                {
                    transform =
                    {
                        name = particleType.ToString(),
                        parent = transform
                    }
                };
                _parents.Add(particleType, parent.transform); 
            }
        }

        public ParticleHolder Create(ParticleType particleType, Vector3 point, Vector3 normal) 
            => Create(particleType, point, Quaternion.LookRotation(normal));

        public ParticleHolder Create(ParticleType particleType, Vector3 position, Quaternion rotation)
        {
            _pool.ExtractElement(particleType, out var particleHolder);
            particleHolder.transform.SetPositionAndRotation(position, rotation);
            return particleHolder;
        }

        private ParticleHolder Instantiate(ParticleType particleType)
        {
            var particleHolder = Instantiate(particleHolderPrefabs[particleType], _parents[particleType]);
            return particleHolder;
        }
    }
}
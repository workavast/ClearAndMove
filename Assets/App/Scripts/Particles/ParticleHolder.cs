using System;
using Avastrad.PoolSystem;
using UnityEngine;

namespace App.Particles
{
    public class ParticleHolder : MonoBehaviour, IPoolable<ParticleHolder>
    {
        [SerializeField] private ParticleProvider particleProvider;
        
        public event Action<ParticleHolder> ReturnElementEvent;
        public event Action<ParticleHolder> DestroyElementEvent;

        private void Awake()
        {
            particleProvider.OnParticleSystemIsOver += () => ReturnElementEvent?.Invoke(this);
        }

        public void OnElementExtractFromPool()
        {
            particleProvider.Play();
        }

        public void OnElementReturnInPool()
        {
            
        }

        private void OnDestroy() 
            => DestroyElementEvent?.Invoke(this);
    }
}
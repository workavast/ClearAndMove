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
#if UNITY_EDITOR
            gameObject.SetActive(true);
#endif
            particleProvider.Play();
        }

        public void OnElementReturnInPool()
        {
#if UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }

        private void OnDestroy() 
            => DestroyElementEvent?.Invoke(this);
    }
}
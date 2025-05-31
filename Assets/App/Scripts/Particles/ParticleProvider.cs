using System;
using UnityEngine;

namespace App.Particles
{
    public class ParticleProvider : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;

        public event Action OnParticleSystemIsOver;
        
        public void Play() 
            => particle.Play();

        private void OnParticleSystemStopped() 
            => OnParticleSystemIsOver?.Invoke();
    }
}
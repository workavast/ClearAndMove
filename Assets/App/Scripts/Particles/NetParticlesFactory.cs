using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace App.Particles
{
    public class NetParticlesFactory : NetworkBehaviour
    {
        [SerializeField] private ParticleFactory particleFactory;

        private readonly Dictionary<byte, ParticleType> _particleTypes =new()
        {
            {1, ParticleType.BulletCollision}
        };
        private readonly Dictionary<ParticleType, byte> _particleBytes =new()
        {
            {ParticleType.BulletCollision, 1}
        };
        
        public void Create(ParticleType particleType, Vector3 point, Vector3 normal) 
            => Create(_particleBytes[particleType], point, normal);

        private void Create(byte particleByte, Vector3 point, Vector3 normal)
            => particleFactory.Create(_particleTypes[particleByte], point, Quaternion.LookRotation(normal));
    }
}
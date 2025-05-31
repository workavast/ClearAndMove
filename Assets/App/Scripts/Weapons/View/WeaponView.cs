using App.Audio.Sources;
using App.Particles;
using UnityEngine;
using Zenject;

namespace App.Weapons.View
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
        [field: SerializeField] public Transform RightHandPoint { get; private set; }
        [field: SerializeField] public Transform LeftHandPoint { get; private set; }
        
        [SerializeField] private Transform barrelPoint;
        [SerializeField] private WeaponViewConfig config;

        [Inject] private readonly AudioFactory _audioFactory;
        [Inject] private readonly ParticleFactory _particleFactory;

        public void ShotVfx()
        {
            _particleFactory.Create(config.ShotSmokePrefab, barrelPoint.position, barrelPoint.rotation);
        }

        public void ShotSfx()
        {
            var pitch = Random.Range(config.MinPitch, config.MaxPitch);
            _audioFactory.Create(config.ShotSfxPrefab, barrelPoint.position, pitch);
        }
    }
}